using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class GroupsRolesView : BaseEntity
	{
		public int Id { get; set; }
		public int GroupId { get; set; }
		public string RoleId { get; set; }
		public string GroupName { get; set; }
		public string RoleDescription { get; set; }
		public string RoleName { get; set; }
		public Nullable<int> DisplayOrder { get; set; }
	}
}
