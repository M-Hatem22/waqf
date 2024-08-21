using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class GovernanceAttachments : BaseEntity
	{
		[Key]
		public long GovernanceAttachmentsId { get; set; }
		public Nullable<int> LkGovernanceId { get; set; }
		public string LkGovernanceNameAr { get; set; }
		public string GovernanceAttachmentsNameAr { get; set; }
		public string GovernanceAttachmentsNameEn { get; set; }
		public byte[] AttachmentFile { get; set; }
		public string AttachmentContent { get; set; }
		public string AttachmentName { get; set; }
		public Nullable<DateTime> UploadedDate { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
	}
}
