using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class AspNetUserLoginsVM
	{
		public string LoginProvider { get; set; }
		public string ProviderKey { get; set; }
		public string UserId { get; set; }
		public string ProviderDisplayName { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
