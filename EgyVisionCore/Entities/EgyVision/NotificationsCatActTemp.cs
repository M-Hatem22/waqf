using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class NotificationsCatActTemp : BaseEntity
	{
		[Key]
		public int Id { get; set; }
		public int TemplateId { get; set; }
		public int NotificationCategoryId { get; set; }
		public int NotificationActionId { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public bool ForSms { get; set; }
		public bool ForEmail { get; set; }
		public bool ForNotification { get; set; }
	}
}
