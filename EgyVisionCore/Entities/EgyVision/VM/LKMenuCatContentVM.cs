using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class LKMenuCatContentVM
	{
		public int ID { get; set; }
		public Nullable<int> MenuCatId { get; set; }
		public string TitleAr { get; set; }
		public string TitleEn { get; set; }
		public string ContentAr { get; set; }
		public string ContentEn { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
        public string HomeAboutUsAr { get; set; }
        public string HomeAboutUEn { get; set; }
    }
}
