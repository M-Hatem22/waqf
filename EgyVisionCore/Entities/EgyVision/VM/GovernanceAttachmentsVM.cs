using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class GovernanceAttachmentsVM
	{
		public long GovernanceAttachmentsId { get; set; }
		public Nullable<int> LkGovernanceId { get; set; }
		public string GovernanceAttachmentsNameAr { get; set; }
		public string LkGovernanceNameAr { get; set; }
		public string GovernanceAttachmentsNameEn { get; set; }
		public byte[] AttachmentFile { get; set; }
		public string AttachmentContent { get; set; }
		public string AttachmentName { get; set; }
		public Nullable<DateTime> UploadedDate { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
