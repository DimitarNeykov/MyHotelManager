namespace MyHotelManager.Web
{
    using System;
    using System.Reflection;

    using Hangfire;
    using Hangfire.Console;
    using Hangfire.Dashboard;
    using Hangfire.SqlServer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MyHotelManager.Common;
    using MyHotelManager.Data;
    using MyHotelManager.Data.Common;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Data.Repositories;
    using MyHotelManager.Data.Seeding;
    using MyHotelManager.Services.CloudinaryManage;
    using MyHotelManager.Services.CronJobs;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Services.Data.Interfaces;
    using MyHotelManager.Services.Data.Services;
    using MyHotelManager.Services.Mapping;
    using MyHotelManager.Services.Messaging;
    using MyHotelManager.Web.ViewModels;
    using Stripe;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(
                config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(
                        this.configuration.GetConnectionString("DefaultConnection"),
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            UsePageLocksOnDequeue = true,
                            DisableGlobalLocks = true,
                        }).UseConsole());

            services.AddHangfireServer();

            services.AddSession();
            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<ICloudinaryService>(serviceProvider =>
                new CloudinaryService(this.configuration["Cloudinary:CloudName"], this.configuration["Cloudinary:ApiKey"],
                    this.configuration["Cloudinary:ApiSecret"]));

            services.AddTransient<IMailHelper>(serviceProvider =>
                new MailHelper(
                    this.configuration["MailSender:SupportEmail"],
                    this.configuration["MailSender:NoReplyEmail"],
                    this.configuration["MailSender:Password"],
                    serviceProvider.GetService<ICloudinaryService>()));

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<ICompaniesService, CompaniesService>();
            services.AddTransient<IHotelsService, HotelsService>();
            services.AddTransient<ICitiesService, CitiesService>();
            services.AddTransient<IStarsService, StarsService>();
            services.AddTransient<IRoomsService, RoomsService>();
            services.AddTransient<IRoomTypesService, RoomTypesService>();
            services.AddTransient<IReservationsService, ReservationsService>();
            services.AddTransient<IGendersService, GendersService>();
            services.AddTransient<IGuestsService, GuestsService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<IClearOldReservation, ClearOldReservations>();
            services.AddTransient<IAboutUsService, AboutUsService>();
            services.AddTransient<IContactUsService, ContactUsService>();
            services.AddTransient<ITourOperatorsService, TourOperatorsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            StripeConfiguration.ApiKey = this.configuration["Stripe:SecretKey"];

            if (env.IsProduction())
            {
                app.UseHangfireServer(new BackgroundJobServerOptions { WorkerCount = 2 });
                app.UseHangfireDashboard(
                    "/hangfire",
                    new DashboardOptions { Authorization = new[] { new HangfireAuthorizationFilter() } });
            }

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder(env.WebRootPath).SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                this.SeedHangfireJobs(recurringJobManager, serviceProvider);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                        endpoints.MapHangfireDashboard();
                    });
        }

        private void SeedHangfireJobs(IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            recurringJobManager.AddOrUpdate(
                "ClearOldReservation",
                () => serviceProvider.GetService<IClearOldReservation>().Clear(),
                Cron.Hourly);
        }

        private class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();
                return httpContext.User.IsInRole(GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
