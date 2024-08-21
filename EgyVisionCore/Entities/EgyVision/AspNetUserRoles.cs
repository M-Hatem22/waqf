using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetUserRoles : BaseEntity
	{
		[Key]
		public string UserId { get; set; }
		[Key]
		public string RoleId { get; set; }
	}
}
