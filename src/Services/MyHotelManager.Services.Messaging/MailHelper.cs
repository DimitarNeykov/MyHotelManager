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

        public bool SendContactForm(string email, string names, string subject, string content)
        {
            var aboutUsInformation = this.aboutUs.All().First();
            try
            {
                var fromAddress = new MailAddress(aboutUsInformation.Email, $"{names} - Contact Form");
                var toAddress = new MailAddress(aboutUsInformation.Email, GlobalConstants.SystemName);

                var body = string.Empty;

                body = "<font size=\"4\">";
                body += $"<b>Email:</b> {email}";
                body += "<br/>";
                body += $"<b>Names:</b> {names}";
                body += "</font>";
                body += "<br/>";
                body += "<br/>";
                body += "<b>Content:</b>";
                body += "<br/>";
                body += content;

                this.SendMessage(fromAddress, toAddress, aboutUsInformation.Email, aboutUsInformation.EmailPassword,
                    subject, body);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendFromIdentity(string email, string subject, string content)
        {
            var aboutUsInformation = this.aboutUs.All().First();
            try
            {
                var fromAddress = new MailAddress(aboutUsInformation.Email, GlobalConstants.SystemName);
                var toAddress = new MailAddress(email);

                this.SendMessage(fromAddress, toAddress, aboutUsInformation.Email, aboutUsInformation.EmailPassword,
                    subject, content);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void SendMessage(MailAddress fromAddress, MailAddress toAddress, string companyEmail,
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
            smtp.Send(message);
        }
    }
}
