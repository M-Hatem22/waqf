using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetUserHalls : BaseEntity
	{
		[Key]
		public string UserId { get; set; }
		[Key]
		public int LKTableTypeId { get; set; }
	}
}
