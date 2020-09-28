namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Models;

    public interface IHotelsService
    {
        Task<int> CreateAsync(string name, int cityId, string address, int starsId, int companyId, ApplicationUser user, string imgUrl);

        IEnumerable<T> GetByUserId<T>(string userId);

        T GetById<T>(int id);
    }
}
