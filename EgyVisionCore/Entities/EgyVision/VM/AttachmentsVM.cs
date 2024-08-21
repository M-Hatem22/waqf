using System;

namespace EgyVisionCore.Entities.EgyVision.VM
{
    public partial class AttachmentsVM
	{
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
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
