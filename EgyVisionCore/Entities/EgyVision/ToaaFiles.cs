using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class ToaaFiles : BaseEntity
	{
		[Key]
		public int id { get; set; }
		public string fileNameAr { get; set; }
		public string fileNameEn { get; set; }
		public byte[] fileContent { get; set; }
		public string fileType { get; set; }
		public Nullable<DateTime> uploadDate { get; set; }
		public Nullable<DateTime> isDeleted { get; set; }
	}
}
