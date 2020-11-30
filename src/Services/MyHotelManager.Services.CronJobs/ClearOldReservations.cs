namespace MyHotelManager.Services.CronJobs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;

    public class ClearOldReservations : IClearOldReservation
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;

        public ClearOldReservations(IDeletableEntityRepository<Reservation> reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public async Task Clear()
        {
            var reservations = this.reservationRepository.All().Where(r => r.ReturnDate.Date < DateTime.Now.Date && r.IsDeleted != true).ToList();
            foreach (var reservation in reservations)
            {
                this.reservationRepository.Delete(reservation);
                await this.reservationRepository.SaveChangesAsync();
            }
        }
    }
}
