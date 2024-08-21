using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKMenuCatContent : BaseEntity
	{
		[Key]
		public int ID { get; set; }
		public string TitleAr { get; set; }
		public string TitleEn { get; set; }
		public string ContentAr { get; set; }
		public string ContentEn { get; set; }
		public Nullable<int> MenuCatId { get; set; }
	}
}
