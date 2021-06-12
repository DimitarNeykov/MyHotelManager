namespace MyHotelManager.Web.ViewModels.TourOperators
{
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class TourOperatorViewModel : IMapFrom<TourOperator>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AgentFirstName { get; set; }

        public string AgentLastName { get; set; }

        public string AgentEmail { get; set; }

        public string AgentPhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyBulstat { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyPhoneNumber { get; set; }
    }
}
