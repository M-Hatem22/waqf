using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LKMenusVM
	{
		public int LKMenuId { get; set; }
		public Nullable<int> ParentId { get; set; }
		public string MenuNameAr { get; set; }
		public string MenuNameEn { get; set; }
		public Nullable<int> DisplayOrder { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
