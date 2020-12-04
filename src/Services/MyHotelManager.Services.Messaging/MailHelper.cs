namespace MyHotelManager.Services.Messaging
{
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using System.Web;

    using Microsoft.EntityFrameworkCore;
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

            var body = HttpUtility.HtmlEncode(content);

            body = body.Replace(" ", "&nbsp;");
            body = body.Replace("\r\n", "<br>");

            var fromAddress = new MailAddress(aboutUsInformation.Email, $"{names} - Contact Form");
            var toAddress = new MailAddress(aboutUsInformation.Email, GlobalConstants.SystemName);

            string bodyBuilder = "<div style=\"background-color: #BADEF7;\"><a href=\"https://localhost:44313\" title=\"HTML Email Check\" target=\"_blank\">"
                + "<p style=\"text-align: center;\">"
                + "<IMG SRC=\"https://scontent.fsof6-1.fna.fbcdn.net/v/t1.15752-9/129001704_383969669609728_4231849282714224807_n.png?_nc_cat=107&ccb=2&_nc_sid=ae9488&_nc_ohc=A4lSDlwICWwAX9128EL&_nc_ht=scontent.fsof6-1.fna&oh=619c3b231eb2d959f18f7ea8354844f3&oe=5FEFD506\" alt=\"Logo\">"
                + "</p>"
                + "</a>"
                + "<p style=\"text-align: center;\">"
                + "<font size=\"5\">"
                + $"<b>Email:</b> {HttpUtility.HtmlEncode(email)}"
                + "<br/>"
                + $"<b>Name:</b> {HttpUtility.HtmlEncode(names)}"
                + "</font>"
                + "</p>"
                + "<hr>"
                + "<font size=\"4\">"
                + "<div style=\"margin-left: 15%; margin-right: 15%;\">"
                + body
                + "</div>"
                + "</font>"
                + "<hr>"
                + "<a href=\"https://localhost:44313\" title=\"HTML Email Check\" target=\"_blank\">"
                + "<IMG SRC=\"https://scontent.fsof6-1.fna.fbcdn.net/v/t1.15752-9/129180179_378261166761462_189153749511486553_n.jpg?_nc_cat=108&ccb=2&_nc_sid=ae9488&_nc_ohc=8PL_zD-_V1kAX_IhS8t&_nc_ht=scontent.fsof6-1.fna&oh=21c23bf78f32a1fb9d2cd6201b2ecb47&oe=5FF19B5A\" alt=\"Logo\">"
                + "</a>"
                + "</div>";

            await this.SendMessageAsync(fromAddress, toAddress, aboutUsInformation.Email, aboutUsInformation.EmailPassword,
                subject, bodyBuilder);
        }

        public async Task SendFromIdentityAsync(string email, string subject, string fullName, string textBeforeButton, string url, string textAfterButton)
        {
            var aboutUsInformation = await this.aboutUs.All().FirstAsync();

            var fromAddress = new MailAddress(aboutUsInformation.Email, GlobalConstants.SystemName);
            var toAddress = new MailAddress(email);

            string bodyBuilder = "<div style=\"background-color: #BADEF7;\"><a href=\"https://localhost:44313\" title=\"HTML Email Check\" target=\"_blank\">"
                + "<p style=\"text-align: center;\">"
                + "<img src=\"https://scontent.fsof6-1.fna.fbcdn.net/v/t1.15752-9/129001704_383969669609728_4231849282714224807_n.png?_nc_cat=107&ccb=2&_nc_sid=ae9488&_nc_ohc=A4lSDlwICWwAX9128EL&_nc_ht=scontent.fsof6-1.fna&oh=619c3b231eb2d959f18f7ea8354844f3&oe=5FEFD506\" alt=\"Logo\">"
                + "</p>"
                + "</a>"
                + "<hr>"
                + "<font size=\"4\">"
                + "<div style=\"margin-left: 15%; margin-right: 15%;\">"
                + "<div style=\"text-align: center;\">"
                + $"Hello {fullName},"
                + "<br>"
                + "<br>"
                + textBeforeButton
                + "<br>"
                + "<br>"
                + $"<a href=\"{url}\" style=\"background-color:#134668;border:1px solid #134668;border-radius:5px;color:#ffffff;display:inline-block;font-size:16px;line-height:44px;text-align:center;text-decoration:none;width:150px;\">Click here</a>"
                + "<br>"
                + "<br>"
                + textAfterButton
                + "</div>"
                + "<br>"
                + "Best regards,"
                + "<br>"
                + "My Hotel Manager Team"
                + "</div>"
                + "</font>"
                + "<hr>"
                + "<a href=\"https://localhost:44313\" title=\"HTML Email Check\" target=\"_blank\">"
                + "<img src=\"https://scontent.fsof6-1.fna.fbcdn.net/v/t1.15752-9/129180179_378261166761462_189153749511486553_n.jpg?_nc_cat=108&ccb=2&_nc_sid=ae9488&_nc_ohc=8PL_zD-_V1kAX_IhS8t&_nc_ht=scontent.fsof6-1.fna&oh=21c23bf78f32a1fb9d2cd6201b2ecb47&oe=5FF19B5A\" alt=\"Logo\">"
                + "</a>"
                + "</div>";

            await this.SendMessageAsync(fromAddress, toAddress, aboutUsInformation.Email, aboutUsInformation.EmailPassword,
                subject, bodyBuilder);
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
