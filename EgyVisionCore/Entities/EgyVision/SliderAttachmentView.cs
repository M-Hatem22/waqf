using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class SliderAttachmentView : BaseEntity
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
	}
}
