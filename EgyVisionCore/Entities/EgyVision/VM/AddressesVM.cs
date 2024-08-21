using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class AddressesVM
	{
		public int AddressId { get; set; }
		public string Mobile { get; set; }
		public string Phone1 { get; set; }
		public string Phone2 { get; set; }
		public string Fax { get; set; }
		public string Email1 { get; set; }
		public string Email2 { get; set; }
		public string Email3 { get; set; }
		public string AddressAr { get; set; }
		public string AddressEn { get; set; }
		public Nullable<double> Lng { get; set; }
		public Nullable<double> Lat { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
