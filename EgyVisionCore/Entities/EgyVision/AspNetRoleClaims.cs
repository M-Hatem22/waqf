using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetRoleClaims : BaseEntity
	{
		[Key]
		public int Id { get; set; }
		public string RoleId { get; set; }
		public string ClaimType { get; set; }
		public string ClaimValue { get; set; }
	}
}
