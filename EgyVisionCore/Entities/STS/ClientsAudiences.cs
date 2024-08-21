using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EgyVisionCore.Entities.STS
{
    public partial class ClientsAudiences : BaseEntity
    {
        [Key]
        public int ClientId { get; set; }
        public string Audience { get; set; }
        public string CientName { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
