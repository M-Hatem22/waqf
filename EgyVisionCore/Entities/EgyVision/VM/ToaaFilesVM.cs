using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class ToaaFilesVM
	{
		public int id { get; set; }
		public string fileNameAr { get; set; }
		public string fileNameEn { get; set; }
		public byte[] fileContent { get; set; }
		public string fileType { get; set; }
		public Nullable<DateTime> uploadDate { get; set; }
		public Nullable<DateTime> isDeleted { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
