namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    using MyHotelManager.Data.Models;

    public interface IHotelsService
    {
        Task CreateAsync(string name, int cityId, string address, int starsId, int companyId, ApplicationUser user);
    }
}
