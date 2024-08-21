using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKAttachmentKeyTypes : BaseEntity
	{
		[Key]
		public int LKKeyTypeId { get; set; }
		public string LKKeyTypeNameAr { get; set; }
		public string LKKeyTypeNameEn { get; set; }
	}
}
