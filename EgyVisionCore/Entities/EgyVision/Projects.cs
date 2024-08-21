using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{ 
	public partial class Projects : BaseEntity
	{
		[Key]
		public long ProjectId { get; set; }
		public string ProjectTitleAr { get; set; }
		public string ProjectTitleEn { get; set; }
		public string ContentAr { get; set; }
		public string ContentEn { get; set; }
	}
}
