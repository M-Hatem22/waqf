using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class DbScriptsVM
	{
		public int Id { get; set; }
		public Nullable<DateTime> ExecuteDate { get; set; }
		public string FileName { get; set; }
		public string ScriptContent { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
