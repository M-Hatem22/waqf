using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LKCountriesVM
	{
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
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
