using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetRoles : BaseEntity
	{
		[Key]
		public string Id { get; set; }
		public string Name { get; set; }
		public string ConcurrencyStamp { get; set; }
		public string NormalizedName { get; set; }
		public string Description { get; set; }
		public Nullable<int> RequestTypeId { get; set; }
		public Nullable<int> DisplayOrder { get; set; }
	}
}
