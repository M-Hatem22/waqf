using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LkNotificationsActionsParameters : BaseEntity
	{
		[Key]
		public int ParameterId { get; set; }
		public Nullable<int> NotificationActionId { get; set; }
		public string ParameterName { get; set; }
		public string ParameterNameAr { get; set; }
	}
}
