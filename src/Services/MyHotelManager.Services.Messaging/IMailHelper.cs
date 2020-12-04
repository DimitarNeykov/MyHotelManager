namespace MyHotelManager.Services.Messaging
{
    using System.Threading.Tasks;

    public interface IMailHelper
    {
        Task SendContactFormAsync(string email, string names, string subject, string content);

        Task SendFromIdentityAsync(string email, string subject, string fullName, string textBeforeButton, string url, string textAfterButton);
    }
}
