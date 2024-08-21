using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class LkGovernance : BaseEntity
	{
		[Key]
		public int LkGovernanceId { get; set; }
		public string LkGovernanceNameAr { get; set; }
		public string LkGovernanceNameEn { get; set; }
	}
}
