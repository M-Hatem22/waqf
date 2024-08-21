using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKCurrencies : BaseEntity
	{
		[Key]
		public int LKCurrencyId { get; set; }
		public string LKCurrencyNameAr { get; set; }
		public string LKCurrencyNameEn { get; set; }
		public string Symbol { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
	}
}
