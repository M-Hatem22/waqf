using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class NotificationsCatActTempVM
	{
		public int Id { get; set; }
		public int TemplateId { get; set; }
		public int NotificationCategoryId { get; set; }
		public int NotificationActionId { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public bool ForSms { get; set; }
		public bool ForEmail { get; set; }
		public bool ForNotification { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
