namespace MyHotelManager.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;

    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public ApplicationUsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task UpdateSelectedHotel(string userId, int hotelId)
        {
            var user = this.userRepository.All().FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return;
            }

            user.SelectedHotelId = hotelId;
            await this.userRepository.SaveChangesAsync();
        }
    }
}
