using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class SocialUrlsVM
	{
		public int SocialUrlId { get; set; }
		public string NameAr { get; set; }
		public string NameEn { get; set; }
		public string Url { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
