using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LKAccountTypes : BaseEntity
	{
		[Key]
		public int LKAccountTypeId { get; set; }
		public string LKAccountTypeNameAr { get; set; }
		public string LKAccountTypeNameEn { get; set; }
		public Nullable<int> ParentId { get; set; }
		public string PrinterName { get; set; }
		public Nullable<long> PartnerId { get; set; }
	}
}
