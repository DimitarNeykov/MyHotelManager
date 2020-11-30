namespace MyHotelManager.Services.CronJobs
{
    using System.Threading.Tasks;

    public interface IClearOldReservation
    {
        Task Clear();
    }
}
