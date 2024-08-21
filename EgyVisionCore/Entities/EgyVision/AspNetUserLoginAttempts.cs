using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetUserLoginAttempts : BaseEntity
	{
		[Key]
		public long Id { get; set; }
		public string UserId { get; set; }
		public byte AttemptsCount { get; set; }
		public DateTime LastAttempt { get; set; }
	}
}
