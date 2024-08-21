using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class ContentsVM
	{
		public long ContentId { get; set; }
		public Nullable<int> LKMenuId { get; set; }
		public string TitleAr { get; set; }
		public string TitleEn { get; set; }
		public string ContentAr { get; set; }
		public string ContentEn { get; set; }
		public Nullable<int> DisplayOrder { get; set; }
		public Nullable<DateTime> TimInsert { get; set; }
		public string UserInsert { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public string UserDeleted { get; set; }
		public Nullable<bool> IsActive { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
