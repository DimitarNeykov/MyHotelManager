namespace MyHotelManager.Data.Models
{
    using System.Collections.Generic;

    using MyHotelManager.Data.Common.Models;

    public class Owner : BaseDeletableModel<int>
    {
        public Owner()
        {
            this.CompanyOwners = new HashSet<CompanyOwner>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<CompanyOwner> CompanyOwners { get; set; }
    }
}
