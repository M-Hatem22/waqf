using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKCountries : BaseEntity
	{
		[Key]
		public int LKCountryId { get; set; }
		public string LKCountryNameAr { get; set; }
		public string LKCountryNameEn { get; set; }
		public Nullable<double> Lng { get; set; }
		public Nullable<double> Lat { get; set; }
		public Nullable<byte> AdminDevisionsLvlsCount { get; set; }
		public Nullable<int> LKCurrencyId { get; set; }
		public string CountryKey { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public Nullable<double> TaxNumber { get; set; }
	}
}
