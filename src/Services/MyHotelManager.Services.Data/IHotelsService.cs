namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Models;

    public interface IHotelsService
    {
        Task CreateAsync(string name, int cityId, string address, int starsId, ApplicationUser user, string imgUrl);

        T GetById<T>(int id);

        Task UpdateAsync(int hotelId, string name, int cityId, string address, int starsId, int cleaningPerDays);
    }
}
