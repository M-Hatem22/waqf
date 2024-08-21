using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LKAttachmentKeyTypesVM
	{
		public int LKKeyTypeId { get; set; }
		public string LKKeyTypeNameAr { get; set; }
		public string LKKeyTypeNameEn { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
