using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyvisionVS.Models
{
    public class LeftSideModelVM
    {
        public LeftSideModelVM()
        {
            LogoImage = new AttachmentsVM();
            ContactUs = new ContactUsVM();
        }
        public AttachmentsVM LogoImage { get; set; }
        public ContactUsVM ContactUs { get; set; }
    }
}
