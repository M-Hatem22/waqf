using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class SQLSessions : BaseEntity
	{
		[Key]
		public string Id { get; set; }
		public byte[] Value { get; set; }
		public DateTimeOffset ExpiresAtTime { get; set; }
		public Nullable<long> SlidingExpirationInSeconds { get; set; }
		public Nullable<DateTimeOffset> AbsoluteExpiration { get; set; }
	}
}
