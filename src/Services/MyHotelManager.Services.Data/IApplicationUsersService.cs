namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    public interface IApplicationUsersService
    {
        Task UpdateSelectedHotel(string userId, int hotelId);
    }
}
