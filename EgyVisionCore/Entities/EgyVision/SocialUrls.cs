using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class SocialUrls : BaseEntity
	{
		[Key]
		public int SocialUrlId { get; set; }
		public string NameAr { get; set; }
		public string NameEn { get; set; }
		public string Url { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
	}
}
