using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetGroups : BaseEntity
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
