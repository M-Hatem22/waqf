using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LKNotificationsTemplatesVM
	{
		public int TemplateId { get; set; }
		public string TemplateTXTAr { get; set; }
		public string TemplateTXTEn { get; set; }
		public Nullable<bool> IsActive { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
