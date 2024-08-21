using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EgyVisionCore.Entities.STS
{
    public partial class AudienceKeys : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string GeneratedKey { get; set; }
        public string Audience { get; set; }
    }
}
