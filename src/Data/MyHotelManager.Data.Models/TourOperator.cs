namespace MyHotelManager.Data.Models
{
    using MyHotelManager.Data.Common.Models;

    public class TourOperator : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int? TourOperatorAgentId { get; set; }

        public TourOperatorAgent Agent { get; set; }

        public int? TourOperatorCompanyId { get; set; }

        public TourOperatorCompany Company { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }
}
