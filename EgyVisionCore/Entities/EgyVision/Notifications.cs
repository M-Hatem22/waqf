using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class Notifications : BaseEntity
	{
		[Key]
		public long NotificationId { get; set; }
		public string NotificationText { get; set; }
		public DateTime NotificationDate { get; set; }
		public string NotificationDateHJ { get; set; }
		public string AddedUserId { get; set; }
		public string TargetUserId { get; set; }
		public Nullable<long> TargetRequestId { get; set; }
		public string TargetRoles { get; set; }
		public string TargetMobileNumber { get; set; }
		public string TargetEmail { get; set; }
		public int NotificationActionId { get; set; }
		public int NotificationTemplateId { get; set; }
		public string NotificationCode { get; set; }
		public Nullable<bool> IsRead { get; set; }
		public bool IsMailSent { get; set; }
		public bool IsMobileSent { get; set; }
		public bool ForSms { get; set; }
		public bool ForEmail { get; set; }
		public bool ForNotification { get; set; }
		public Nullable<DateTime> SmsProcessingDate { get; set; }
	}
}
