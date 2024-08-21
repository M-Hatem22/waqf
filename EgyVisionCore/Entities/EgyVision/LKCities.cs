using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKCities : BaseEntity
	{
		[Key]
		public long LKCityId { get; set; }
		public string LKCityNameAr { get; set; }
		public string LKCityNameEn { get; set; }
		public Nullable<long> LKRegionId { get; set; }
		public Nullable<int> LKCountryId { get; set; }
	}
}
