namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    using MyHotelManager.Data.Models;

    public interface ICompaniesService
    {
        Task CreateAsync(string name, string bulstat, string phoneNumber, string email, int cityId, string address, ApplicationUser user);
    }
}
