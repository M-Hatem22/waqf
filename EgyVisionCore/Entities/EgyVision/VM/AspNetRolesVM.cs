using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class AspNetRolesVM
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ConcurrencyStamp { get; set; }
		public string NormalizedName { get; set; }
		public string Description { get; set; }
		public Nullable<int> RequestTypeId { get; set; }
		public Nullable<int> DisplayOrder { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
