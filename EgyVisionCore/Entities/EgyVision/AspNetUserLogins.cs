using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class AspNetUserLogins : BaseEntity
	{
		[Key]
		public string LoginProvider { get; set; }
		[Key]
		public string ProviderKey { get; set; }
		[Key]
		public string UserId { get; set; }
		public string ProviderDisplayName { get; set; }
	}
}
