namespace MyHotelManager.Data.Models
{
    using MyHotelManager.Data.Common.Models;

    public class CompanyOwner : BaseDeletableModel<int>
    {
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public int OwnerId { get; set; }

        public virtual Owner Owner { get; set; }
    }
}
