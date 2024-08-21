using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LKAccountTypesVM
	{
		public int LKAccountTypeId { get; set; }
		public string LKAccountTypeNameAr { get; set; }
		public string LKAccountTypeNameEn { get; set; }
		public Nullable<int> ParentId { get; set; }
		public string PrinterName { get; set; }
		public Nullable<long> PartnerId { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
