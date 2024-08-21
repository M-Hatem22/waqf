using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKRegions : BaseEntity
	{
		[Key]
		public long LKRegionId { get; set; }
		public string LKRegionNameAr { get; set; }
		public string LKRegionNameEn { get; set; }
		public Nullable<int> LKCountryId { get; set; }
	}
}
