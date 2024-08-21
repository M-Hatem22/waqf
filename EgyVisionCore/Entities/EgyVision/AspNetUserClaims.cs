using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetUserClaims : BaseEntity
	{
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public string ClaimType { get; set; }
		public string ClaimValue { get; set; }
	}
}
