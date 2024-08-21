using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LKCurrenciesVM
	{
		public int LKCurrencyId { get; set; }
		public string LKCurrencyNameAr { get; set; }
		public string LKCurrencyNameEn { get; set; }
		public string Symbol { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
