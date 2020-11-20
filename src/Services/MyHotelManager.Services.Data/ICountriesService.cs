namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;

    public interface ICountriesService
    {
        IEnumerable<T> GetAll<T>();
    }
}
