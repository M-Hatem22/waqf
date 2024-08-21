using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetUsers : BaseEntity
	{
		[Key]
		public string Id { get; set; }
		public string Email { get; set; }
		public bool EmailConfirmed { get; set; }
		public string PasswordHash { get; set; }
		public string SecurityStamp { get; set; }
		public string ConcurrencyStamp { get; set; }
		public Nullable<DateTimeOffset> LockoutEnd { get; set; }
		public string NormalizedEmail { get; set; }
		public string NormalizedUserName { get; set; }
		public string PhoneNumber { get; set; }
		public bool PhoneNumberConfirmed { get; set; }
		public bool TwoFactorEnabled { get; set; }
		public Nullable<DateTime> LockoutEndDateUtc { get; set; }
		public bool LockoutEnabled { get; set; }
		public int AccessFailedCount { get; set; }
		public string UserName { get; set; }
		public string NationalId { get; set; }
		public Nullable<DateTime> LastLogin { get; set; }
		public Nullable<DateTime> RegisterDate { get; set; }
		public Nullable<bool> IsActive { get; set; }
		public Nullable<bool> IsMobileActivated { get; set; }
		public string EmployeeCode { get; set; }
		public string FullName { get; set; }
		public string VerCode { get; set; }
		public Nullable<DateTime> VerCodeExpireDate { get; set; }
		public Nullable<byte> AccountType { get; set; }
		public Nullable<int> LKCountryId { get; set; }
		public Nullable<int> ServiceCountryId { get; set; }
		public Nullable<int> Points { get; set; }
		public Nullable<long> PartnerId { get; set; }
		public Nullable<long> CharityId { get; set; }
	}
}
