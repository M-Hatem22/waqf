using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
	public partial class SQLSessionsVM
	{
		public string Id { get; set; }
		public byte[] Value { get; set; }
		public DateTimeOffset ExpiresAtTime { get; set; }
		public Nullable<long> SlidingExpirationInSeconds { get; set; }
		public Nullable<DateTimeOffset> AbsoluteExpiration { get; set; }
		public int jtStartIndex { get; set; }
		public int jtPageSize { get; set; }
		public string jtSorting { get; set; }
		public int TotalRecordCount { get; set; }
		public string OrderBy { get; set; }
		public bool OrderByReversed { get; set; }
	}
}
