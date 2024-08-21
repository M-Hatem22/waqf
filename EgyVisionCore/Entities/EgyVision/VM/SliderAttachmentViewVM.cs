using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class SliderAttachmentViewVM
	{
		[Key]
		public int SliderId { get; set; }
		public string SliderTitleAr { get; set; }
		public string SliderTitleEn { get; set; }
		public string ContentAr { get; set; }
		public string ContentEn { get; set; }
		public byte[] AttachmentFile { get; set; }
		public Nullable<long> KeyId { get; set; }
		public Nullable<int> LKKeyTypeId { get; set; }
		public Nullable<int> LKAttachmentTypeId { get; set; }
		public bool MainSlider { get; set; }
		public long AttachmentId { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
