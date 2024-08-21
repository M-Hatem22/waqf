using System;
using System.Collections.Generic;
using System.Text;

namespace EgyVisionCore.Entities.STS.VM
{
    public partial class AudienceKeysVM
    {
        public int Id { get; set; }
        public string GeneratedKey { get; set; }
        public string Audience { get; set; }
        public int jtStartIndex { get; set; }
        public int jtPageSize { get; set; }
        public string jtSorting { get; set; }
        public int TotalRecordCount { get; set; }
    }
}
