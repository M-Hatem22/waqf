using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class Sliders : BaseEntity
	{
		[Key]
		public int SliderId { get; set; }
		public string SliderTitleAr { get; set; }
		public string SliderTitleEn { get; set; }
		public string ContentAr { get; set; }
		public string ContentEn { get; set; }
		public bool MainSlider { get; set; }
	}
}
