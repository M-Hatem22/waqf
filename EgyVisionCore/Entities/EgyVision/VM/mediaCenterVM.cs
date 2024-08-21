using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class mediaCenterVM
	{
		public int id { get; set; }
		public string title { get; set; }
		public byte[] image { get; set; }
		public string article { get; set; }
		public string link { get; set; }
		public Nullable<DateTime> dateAdded { get; set; }
		public Nullable<DateTime> isDeleted { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
