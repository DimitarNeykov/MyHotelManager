namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    public interface ICompanyService
    {
        Task CreateAsync(string name, string bulstat, string phoneNumber, string email, string address, string userId);
    }
}
