namespace MyHotelManager.Data.Models
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Company : BaseDeletableModel<int>
    {
        public Company()
        {
            this.CompanyOwners = new HashSet<CompanyOwner>();
        }

        public string Name { get; set; }

        public string Bulstat { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public virtual ICollection<CompanyOwner> CompanyOwners { get; set; }
    }
}
