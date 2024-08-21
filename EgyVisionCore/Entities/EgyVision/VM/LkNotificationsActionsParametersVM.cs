using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LkNotificationsActionsParametersVM
	{
		public int ParameterId { get; set; }
		public Nullable<int> NotificationActionId { get; set; }
		public string ParameterName { get; set; }
		public string ParameterNameAr { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
