using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LKAttachmentTypesVM
	{
		public int LKAttachmentTypeId { get; set; }
		public string LKAttachmentTypeNameAr { get; set; }
		public string LKAttachmentTypeNameEn { get; set; }
		public Nullable<int> LKAttachmentKeyTypeId { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}