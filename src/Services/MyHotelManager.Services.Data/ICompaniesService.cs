namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    public interface ICompaniesService
    {
        Task CreateAsync(string name, string bulstat, string phoneNumber, string email, int cityId, string address, int hotelId);

        Task<T> GetById<T>(int id);

        Task EditAsync(int id, int hotelId, string name, string bulstat, string phoneNumber, string email, int cityId, string address);
    }
}
