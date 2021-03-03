namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;

    public interface IStarsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
