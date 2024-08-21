using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class UsersRolesViewVM
	{
		public string UserId { get; set; }
		public string RoleId { get; set; }
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
		public string NationalID { get; set; }
		public Nullable<DateTime> LastLogin { get; set; }
		public Nullable<DateTime> RegisterDate { get; set; }
		public Nullable<bool> IsActive { get; set; }
		public string EmployeeCode { get; set; }
		public string FullName { get; set; }
		public string RoleName { get; set; }
		public string NormalizedName { get; set; }
		public string Description { get; set; }
		public Nullable<int> RequestTypeId { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
