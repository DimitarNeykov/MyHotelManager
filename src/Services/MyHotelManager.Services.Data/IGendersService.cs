namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;

    public interface IGendersService
    {
        IEnumerable<T> GetAll<T>();
    }
}
