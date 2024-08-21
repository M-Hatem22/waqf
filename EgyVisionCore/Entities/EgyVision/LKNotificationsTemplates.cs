using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKNotificationsTemplates : BaseEntity
	{
		[Key]
		public int TemplateId { get; set; }
		public string TemplateTXTAr { get; set; }
		public string TemplateTXTEn { get; set; }
		public Nullable<bool> IsActive { get; set; }
	}
}
