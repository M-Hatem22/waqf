using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class DbScripts : BaseEntity
	{
		[Key]
		public int Id { get; set; }
		public Nullable<DateTime> ExecuteDate { get; set; }
		public string FileName { get; set; }
		public string ScriptContent { get; set; }
	}
}
