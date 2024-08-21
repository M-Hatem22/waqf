using Microsoft.AspNetCore.Identity;
using System;

namespace EgyVisionCore.Entities.EgyVision
{
    public class AppUser : IdentityUser
    {
        public Nullable<bool> IsMobileActivated { get; set; }
        public string NationalId { get; set; }
        public Nullable<long> PartnerId { get; set; }
        public Nullable<int> LKCountryId { get; set; }
        public Nullable<int> ServiceCountryId { get; set; }
        public Nullable<DateTime> RegisterDate { get; set; }
        public Nullable<DateTime> LastLogin { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public Nullable<Byte> AccountType { get; set; }
        public Nullable<int> Points { get; set; }

    }
}
