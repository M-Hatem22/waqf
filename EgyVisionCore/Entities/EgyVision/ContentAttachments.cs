using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class ContentAttachments : BaseEntity
	{
		public Nullable<long> ContentAttachmentId { get; set; }
		public Nullable<long> ContentId { get; set; }
		public byte[] AttachmentFile { get; set; }
		public string AttachmentContent { get; set; }
		public string AttachmentName { get; set; }
		public Nullable<DateTime> UploadedDate { get; set; }
		public Nullable<int> DisplayOrder { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
	}
}
