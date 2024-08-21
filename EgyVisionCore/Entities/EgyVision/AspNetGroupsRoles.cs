using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetGroupsRoles : BaseEntity
	{
		[Key]
		public int Id { get; set; }
		public int GroupId { get; set; }
		public string RoleId { get; set; }
	}
}
