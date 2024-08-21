using System;
using System.Collections.Generic;
using System.Text;

namespace EgyVisionCore.Entities.STS.VM
{
    public partial class ClientsAudiencesVM
    {
        public int ClientId { get; set; }
        public string Audience { get; set; }
        public string CientName { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public int jtStartIndex { get; set; }
        public int jtPageSize { get; set; }
        public string jtSorting { get; set; }
        public int TotalRecordCount { get; set; }
    }
}
