using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class GroupsRolesViewVM
	{
		public int Id { get; set; }
		public int GroupId { get; set; }
		public string RoleId { get; set; }
		public string GroupName { get; set; }
		public string RoleDescription { get; set; }
		public string RoleName { get; set; }
		public Nullable<int> DisplayOrder { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
