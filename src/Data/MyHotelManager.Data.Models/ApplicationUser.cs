﻿// ReSharper disable VirtualMemberCallInConstructor
namespace MyHotelManager.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using MyHotelManager.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Payments = new List<Payment>();
            this.CreatorReservations = new HashSet<Reservation>();
            this.EditorReservations = new HashSet<Reservation>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int? GenderId { get; set; }

        public Gender Gender { get; set; }

        public int? HotelId { get; set; }

        public Hotel Hotel { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Reservation> CreatorReservations { get; set; }

        public ICollection<Reservation> EditorReservations { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

        public ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
