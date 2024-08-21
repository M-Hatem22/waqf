using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class UsersAddressesVM
	{
		public long AddressId { get; set; }
		public string UserId { get; set; }
		public Nullable<bool> IsMain { get; set; }
		public Nullable<int> LKCountryId { get; set; }
		public Nullable<long> LKRegionId { get; set; }
		public Nullable<long> LKCityId { get; set; }
		public Nullable<long> LKDistrictId { get; set; }
		public string FullAddressAr { get; set; }
		public string FullAddressEn { get; set; }
		public string StreetAr { get; set; }
		public string StreetEn { get; set; }
		public string PostalCode { get; set; }
		public string AdditionalCode { get; set; }
		public string BuildingNo { get; set; }
		public string UnitNo { get; set; }
		public Nullable<double> Lng { get; set; }
		public Nullable<double> Lat { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public string TitleAr { get; set; }
		public string TitleEn { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
