using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKAttachmentTypes : BaseEntity
	{
		[Key]
		public int LKAttachmentTypeId { get; set; }
		public string LKAttachmentTypeNameAr { get; set; }
		public string LKAttachmentTypeNameEn { get; set; }
		public Nullable<int> LKAttachmentKeyTypeId { get; set; }
	}
}
