using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetGroupsUsers : BaseEntity
	{
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public int GroupId { get; set; }
	}
}
