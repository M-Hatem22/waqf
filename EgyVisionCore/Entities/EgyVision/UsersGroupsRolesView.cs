using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class UsersGroupsRolesView : BaseEntity
	{
		public string UserId { get; set; }
		public string EmployeeCode { get; set; }
		public string GroupName { get; set; }
		public string RoleDescription { get; set; }
		public string RoleName { get; set; }
		public int GroupId { get; set; }
		public string RoleId { get; set; }
	}
}
