using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class mediaCenter : BaseEntity
	{
		[Key]
		public int id { get; set; }
		public string title { get; set; }
		public byte[] image { get; set; }
		public string article { get; set; }
		public string link { get; set; }
		public Nullable<DateTime> dateAdded { get; set; }
		public Nullable<DateTime> isDeleted { get; set; }
	}
}
