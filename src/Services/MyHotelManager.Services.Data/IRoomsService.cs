namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    public interface IRoomsService
    {
        Task CreateAsync(string number, int roomTypeId, decimal price, string description, int hotelId);
    }
}
