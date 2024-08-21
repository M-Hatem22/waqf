using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKDistricts : BaseEntity
	{
		[Key]
		public long LKDistrictId { get; set; }
		public string LKDistrictNameAr { get; set; }
		public string LKDistrictNameEn { get; set; }
		public Nullable<long> LKCityId { get; set; }
		public Nullable<int> LKCountryId { get; set; }
	}
}
