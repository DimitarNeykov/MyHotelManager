using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyHotelManager.Services.Messaging
{
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    using MyHotelManager.Common;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;

    public class MailHelper : IMailHelper
    {
        private readonly IDeletableEntityRepository<AboutUs> aboutUs;

        public MailHelper(IDeletableEntityRepository<AboutUs> aboutUs)
        {
            this.aboutUs = aboutUs;
        }

        public async Task SendContactFormAsync(string email, string names, string subject, string content)
        {
            var aboutUsInformation = await this.aboutUs.All().FirstAsync();

            var fromAddress = new MailAddress(aboutUsInformation.Email, $"{names} - Contact Form");
            var toAddress = new MailAddress(aboutUsInformation.Email, GlobalConstants.SystemName);

            string body = "<font size=\"4\">";
            body += $"<b>Email:</b> {email}";
            body += "<br/>";
            body += $"<b>Names:</b> {names}";
            body += "</font>";
            body += "<br/>";
            body += "<br/>";
            body += "<b>Content:</b>";
            body += "<br/>";
            body += content;

            await this.SendMessageAsync(fromAddress, toAddress, aboutUsInformation.Email, aboutUsInformation.EmailPassword,
                subject, body);
        }

        public async Task SendFromIdentityAsync(string email, string subject, string content)
        {
            var aboutUsInformation = await this.aboutUs.All().FirstAsync();
            var fromAddress = new MailAddress(aboutUsInformation.Email, GlobalConstants.SystemName);
            var toAddress = new MailAddress(email);

            await this.SendMessageAsync(fromAddress, toAddress, aboutUsInformation.Email, aboutUsInformation.EmailPassword,
                subject, content);
        }

        private async Task SendMessageAsync(MailAddress fromAddress, MailAddress toAddress, string companyEmail,
            string companyEmailPassword, string subject, string body)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(companyEmail, companyEmailPassword),
            };
            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
            };

            message.IsBodyHtml = true;

            await smtp.SendMailAsync(message);
        }
    }
}
