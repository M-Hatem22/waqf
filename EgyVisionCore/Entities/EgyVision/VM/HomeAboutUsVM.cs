using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class HomeAboutUsVM
	{
		public long HomeAboutUsId { get; set; }
		public string HomeAboutUsAr { get; set; }
		public string HomeAboutUEn { get; set; }
		public Nullable<DateTime> TimeInsert { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
