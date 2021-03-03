namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;

    public interface ICitiesService
    {
        IEnumerable<T> GetAll<T>();
    }
}
