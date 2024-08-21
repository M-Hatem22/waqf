using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKMenus : BaseEntity
	{
		[Key]
		public int LKMenuId { get; set; }
		public Nullable<int> ParentId { get; set; }
		public string MenuNameAr { get; set; }
		public string MenuNameEn { get; set; }
		public Nullable<int> DisplayOrder { get; set; }
		public Nullable<DateTime> Deleted { get; set; }
	}
}
