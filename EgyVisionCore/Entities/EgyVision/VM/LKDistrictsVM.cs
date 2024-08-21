using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LKDistrictsVM
	{
		public long LKDistrictId { get; set; }
		public string LKDistrictNameAr { get; set; }
		public string LKDistrictNameEn { get; set; }
		public Nullable<long> LKCityId { get; set; }
		public Nullable<int> LKCountryId { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
