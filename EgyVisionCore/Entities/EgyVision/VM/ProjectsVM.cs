using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class ProjectsVM
	{
		public long ProjectId { get; set; }
		public string ProjectTitleAr { get; set; }
		public string ProjectTitleEn { get; set; }
		public string ContentAr { get; set; }
		public string ContentEn { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
		public byte[] CoverPicture { get; set; }
        public List<AttachmentsVM> GalleryPictures { get; set; }
    }
}
