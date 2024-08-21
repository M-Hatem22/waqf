using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
	public partial class ContactUs : BaseEntity
	{
		[Key]
		public int ID { get; set; }
		public string PhoneNumber { get; set; }
		public string MobileNumber { get; set; }
		public string Email { get; set; }
		public string TitleAr { get; set; }
		public string TitleEn { get; set; }
		public string GoogleURL { get; set; }
		public string FacebookUrl { get; set; }
		public string GooglePlus { get; set; }
		public string InstagramUrl { get; set; }
		public string YoutubeUrl { get; set; }
		public string LinkedinUrl { get; set; }
		public string TwitterUrl { get; set; }
		public Nullable<double> Latitude { get; set; }
		public Nullable<double> Longitude { get; set; }
	}
}
