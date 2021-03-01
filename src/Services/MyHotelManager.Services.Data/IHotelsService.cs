namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    using MyHotelManager.Data.Models;

    public interface IHotelsService
    {
        Task CreateAsync(string name, int cityId, string address, int starsId, int cleaningPerDays, ApplicationUser user);

        Task SetPayTrue(int? hotelId);

        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetByIdWithDeletedAsync<T>(int id);

        Task UpdateAsync(int hotelId, string name, int cityId, string address, int starsId, int cleaningPerDays);
    }
}
