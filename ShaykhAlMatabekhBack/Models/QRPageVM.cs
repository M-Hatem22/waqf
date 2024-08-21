using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEgyVision.Models
{
    public class QRPageVM
    {
        public QRPageVM()
        {
            CodeList = new List<string>();
        }

        public string UserId { get; set; }
        public List<string> CodeList { get; set; }
        public string HostName { get; set; }
        public string DomainName { get; set; }
    }

    public class QRPageDisplayVM
    {
        public string userId { get; set; }
        public string QR { get; set; }
    }
}
