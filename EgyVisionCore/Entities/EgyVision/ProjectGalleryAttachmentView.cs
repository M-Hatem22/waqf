using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class ProjectGalleryAttachmentView : BaseEntity
	{
		[Key]
		public long ProjectId { get; set; }
		public string ProjectTitleAr { get; set; }
		public string ProjectTitleEn { get; set; }
		public byte[] AttachmentFile { get; set; }
		public long AttachmentId { get; set; }
		public Nullable<int> LKKeyTypeId { get; set; }
		public Nullable<int> LKAttachmentTypeId { get; set; }
		public string AttachmentName { get; set; }
		public string AttachmentContent { get; set; }
		public string KeyIdStr { get; set; }
		public string ContentAr { get; set; }
		public string ContentEn { get; set; }
	}
}
