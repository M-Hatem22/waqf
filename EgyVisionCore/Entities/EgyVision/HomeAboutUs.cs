using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class HomeAboutUs : BaseEntity
	{
		[Key]
		public long HomeAboutUsId { get; set; }
		public string HomeAboutUsAr { get; set; }
		public string HomeAboutUEn { get; set; }
		public Nullable<DateTime> TimeInsert { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
	}
}
