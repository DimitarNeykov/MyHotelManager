namespace MyHotelManager.Services.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data.DTOs.TourOperators;
    using MyHotelManager.Services.Data.Interfaces;
    using MyHotelManager.Services.Mapping;

    public class TourOperatorsService : ITourOperatorsService
    {
        private readonly IDeletableEntityRepository<TourOperator> tourOperatorRepository;

        public TourOperatorsService(IDeletableEntityRepository<TourOperator> tourOperatorRepository)
        {
            this.tourOperatorRepository = tourOperatorRepository;
        }

        public async Task CreateAsync(TourOperatorCreateDto input)
        {
            var agent = new TourOperatorAgent
            {
                FirstName = input.Agent.FirstName,
                LastName = input.Agent.LastName,
                Email = input.Agent.Email,
                PhoneNumber = input.Agent.PhoneNumber,
            };

            var company = new TourOperatorCompany
            {
                Name = input.Company.Name,
                Bulstat = input.Company.Bulstat,
                Email = input.Company.Email,
                PhoneNumber = input.Company.PhoneNumber,
            };

            var tourOperator = new TourOperator
            {
                Name = input.Name,
                HotelId = input.HotelId,
                Agent = agent,
                Company = company,
            };

            await this.tourOperatorRepository.AddAsync(tourOperator);
            await this.tourOperatorRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int tourOperatorId)
        {
            var reservation = await this.tourOperatorRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == tourOperatorId);

            this.tourOperatorRepository.Delete(reservation);

            await this.tourOperatorRepository.SaveChangesAsync();
        }

        public async Task EditAsync(TourOperatorEditDto input)
        {
            var tourOperator = this.tourOperatorRepository
                .All()
                .Include(x => x.Agent)
                .Include(x => x.Company)
                .FirstOrDefault(x => x.Id == input.Id);

            tourOperator.Name = input.Name;
            if (tourOperator.Agent != null)
            {
                tourOperator.Agent.FirstName = input.Agent.FirstName;
                tourOperator.Agent.LastName = input.Agent.LastName;
                tourOperator.Agent.PhoneNumber = input.Agent.PhoneNumber;
                tourOperator.Agent.Email = input.Agent.Email;
            }
            else
            {
                var agent = new TourOperatorAgent
                {
                    FirstName = input.Agent.FirstName,
                    LastName = input.Agent.LastName,
                    Email = input.Agent.Email,
                    PhoneNumber = input.Agent.PhoneNumber,
                };

                tourOperator.Agent = agent;
            }

            if (tourOperator.Company != null)
            {
                tourOperator.Company.Name = input.Company.Name;
                tourOperator.Company.Bulstat = input.Company.Bulstat;
                tourOperator.Company.PhoneNumber = input.Company.PhoneNumber;
                tourOperator.Company.Email = input.Company.Email;
            }
            else
            {
                var company = new TourOperatorCompany
                {
                    Name = input.Company.Name,
                    Bulstat = input.Company.Bulstat,
                    Email = input.Company.Email,
                    PhoneNumber = input.Company.PhoneNumber,
                };

                tourOperator.Company = company;
            }

            await this.tourOperatorRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByHotelId<T>(int hotelId)
        {
            var tourOperators = this.tourOperatorRepository
                .All()
                .Where(x => x.HotelId == hotelId)
                .To<T>()
                .ToList();

            return tourOperators;
        }

        public T GetById<T>(int tourOperatorId)
        {
            var tourOperator = this.tourOperatorRepository
                .All()
                .Where(x => x.Id == tourOperatorId)
                .To<T>()
                .FirstOrDefault();

            return tourOperator;
        }
    }
}
