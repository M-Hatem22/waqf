using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class ProjectCoverAttachmentViewVM
	{
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
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
