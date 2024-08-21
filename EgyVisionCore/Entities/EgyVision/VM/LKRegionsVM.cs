using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LKRegionsVM
	{
		public long LKRegionId { get; set; }
		public string LKRegionNameAr { get; set; }
		public string LKRegionNameEn { get; set; }
		public Nullable<int> LKCountryId { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
