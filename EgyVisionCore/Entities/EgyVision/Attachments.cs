using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class Attachments : BaseEntity
	{
		[Key]
		public long AttachmentId { get; set; }
		public byte[] AttachmentFile { get; set; }
		public string AttachmentContent { get; set; }
		public string AttachmentName { get; set; }
		public Nullable<DateTime> UploadedDate { get; set; }
		public Nullable<long> KeyId { get; set; }
		public string KeyIdStr { get; set; }
		public Nullable<int> LKAttachmentTypeId { get; set; }
		public Nullable<int> LKKeyTypeId { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
	}
}
