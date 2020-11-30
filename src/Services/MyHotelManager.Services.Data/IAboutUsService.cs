namespace MyHotelManager.Services.Data
{
    using System.Threading.Tasks;

    public interface IAboutUsService
    {
        Task<T> GetInformationAsync<T>();
    }
}
