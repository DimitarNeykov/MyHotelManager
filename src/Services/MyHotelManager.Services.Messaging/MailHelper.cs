namespace MyHotelManager.Services.Messaging
{
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using System.Web;

    using MyHotelManager.Common;
    using MyHotelManager.Services.CloudinaryManage;

    public class MailHelper : IMailHelper
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly string supportEmail;
        private readonly string noReplyEmail;
        private readonly string password;

        public MailHelper(string supportEmail, string noReplyEmail, string password, ICloudinaryService cloudinaryService)
        {
            this.supportEmail = supportEmail;
            this.noReplyEmail = noReplyEmail;
            this.password = password;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task SendContactFormAsync(string email, string names, string subject, string content)
        {
            var body = HttpUtility.HtmlEncode(content);

            body = body.Replace(" ", "&nbsp;");
            body = body.Replace("\r\n", "<br>");

            var fromAddress = new MailAddress(this.supportEmail, $"{names} - Contact Form");
            var toAddress = new MailAddress(this.supportEmail, GlobalConstants.SystemName);

            var headerImg = this.cloudinaryService.GetImgByName("email_snw4un.png");
            var footerImg = this.cloudinaryService.GetImgByName("email_footer1_rqljpo.jpg");

            string bodyBuilder = "<div style=\"background-color: #BADEF7;\"><a href=\"https://localhost:44313\" title=\"HTML Email Check\" target=\"_blank\">"
                + "<p style=\"text-align: center;\">"
                + $"<img src=\"{headerImg}\" alt=\"Logo\">"
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
                + $"<img src=\"{footerImg}\" alt=\"Logo\">"
                + "</a>"
                + "</div>";

            await this.SendMessageAsync(fromAddress, toAddress, subject, bodyBuilder);
        }

        public async Task SendFromIdentityAsync(string email, string subject, string fullName, string textBeforeButton, string url, string textAfterButton)
        {
            var fromAddress = new MailAddress(this.noReplyEmail, GlobalConstants.SystemName);
            var toAddress = new MailAddress(email);

            var headerImg = this.cloudinaryService.GetImgByName("email_snw4un.png");
            var footerImg = this.cloudinaryService.GetImgByName("email_footer1_rqljpo.jpg");

            string bodyBuilder = "<div style=\"background-color: #BADEF7;\"><a href=\"https://localhost:44313\" title=\"HTML Email Check\" target=\"_blank\">"
                + "<p style=\"text-align: center;\">"
                + $"<img src=\"{headerImg}\" alt =\"Logo\">"
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
                + $"<img src=\"{footerImg}\" alt=\"Logo\">"
                + "</a>"
                + "</div>";

            await this.SendMessageAsync(fromAddress, toAddress, subject, bodyBuilder);
        }

        private async Task SendMessageAsync(MailAddress fromAddress, MailAddress toAddress, string subject, string body)
        {
            var smtp = new SmtpClient
            {
                Host = "plesk5000.is.cc",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(this.noReplyEmail, this.password),
            };
            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            await smtp.SendMailAsync(message);
        }
    }
}
