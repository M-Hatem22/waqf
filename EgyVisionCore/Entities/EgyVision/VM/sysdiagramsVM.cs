using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class sysdiagramsVM
	{
		public string name { get; set; }
		public int principal_id { get; set; }
		public int diagram_id { get; set; }
		public Nullable<int> version { get; set; }
		public byte[] definition { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
