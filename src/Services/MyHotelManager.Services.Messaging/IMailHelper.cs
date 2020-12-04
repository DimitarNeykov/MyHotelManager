namespace MyHotelManager.Services.Messaging
{
    public interface IMailHelper
    {
        bool SendContactForm(string email, string names, string subject, string content);

        bool SendFromIdentity(string email, string subject, string content);
    }
}
