namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;

    public interface IRoomTypesService
    {
        IEnumerable<T> GetAll<T>();
    }
}
