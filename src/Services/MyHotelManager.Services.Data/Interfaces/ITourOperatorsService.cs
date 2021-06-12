namespace MyHotelManager.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelManager.Services.Data.DTOs.TourOperators;

    public interface ITourOperatorsService
    {
        Task CreateAsync(TourOperatorCreateDto input);

        IEnumerable<T> GetAllByHotelId<T>(int hotelId);

        T GetById<T>(int tourOperatorId);

        Task EditAsync(TourOperatorEditDto input);
    }
}
