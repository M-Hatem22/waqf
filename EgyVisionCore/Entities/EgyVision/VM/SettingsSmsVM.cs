using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class SettingsSmsVM
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public bool EnableSms { get; set; }
		public Nullable<DateTime> BalanceLastCheckDate { get; set; }
		public Nullable<int> CheckedBalance { get; set; }
		public Nullable<int> MinmumBalanceToAlert { get; set; }
		public string ClientMobile { get; set; }
		public string Sender { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
