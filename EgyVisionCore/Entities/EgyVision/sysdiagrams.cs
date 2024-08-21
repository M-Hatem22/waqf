using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class sysdiagrams : BaseEntity
	{
		public string name { get; set; }
		public int principal_id { get; set; }
		[Key]
		public int diagram_id { get; set; }
		public Nullable<int> version { get; set; }
		public byte[] definition { get; set; }
	}
}
