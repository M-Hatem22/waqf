using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKNotificationsCategory : BaseEntity
	{
		[Key]
		public int CategoryId { get; set; }
		public string CategoryTxt { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
	}
}
