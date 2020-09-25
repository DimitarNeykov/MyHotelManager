namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompaniesService
    {
        Task CreateAsync(string name, string bulstat, string phoneNumber, string email, string address, string userId);

        IEnumerable<T> GetAllByUserId<T>(string userId);
    }
}
