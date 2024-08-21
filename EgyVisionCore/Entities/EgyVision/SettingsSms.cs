using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class SettingsSms : BaseEntity
	{
		[Key]
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public bool EnableSms { get; set; }
		public Nullable<DateTime> BalanceLastCheckDate { get; set; }
		public Nullable<int> CheckedBalance { get; set; }
		public Nullable<int> MinmumBalanceToAlert { get; set; }
		public string ClientMobile { get; set; }
		public string Sender { get; set; }
	}
}
