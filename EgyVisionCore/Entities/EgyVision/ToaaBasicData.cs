using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class ToaaBasicData : BaseEntity
	{
		[Key]
		public int id { get; set; }
		public string description { get; set; }
		public string data { get; set; }
		public DateTime lastChange { get; set; }
	}
}
