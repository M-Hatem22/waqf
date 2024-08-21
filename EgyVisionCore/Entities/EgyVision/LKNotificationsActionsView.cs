using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKNotificationsActionsView : BaseEntity
	{
		public int NotificationActionId { get; set; }
		public string NotificationActionDescription { get; set; }
		public Nullable<int> NotificationCategoryId { get; set; }
		public string System { get; set; }
		public string SystemFrom { get; set; }
		public string Controller { get; set; }
		public string ControllerAr { get; set; }
		public string TargetUsers { get; set; }
		public string Action { get; set; }
		public string ActionAr { get; set; }
		public string ActionCode { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public string CategoryTxt { get; set; }
	}
}
