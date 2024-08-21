using EgyVisionCore.Entities.EgyVision.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyvisionVS.Models
{
    public class FooterVM
    {
        public FooterVM()
        {
            AboutUs = new LKMenuCatContentVM();
            ContactUs = new ContactUsVM();
        }
        public LKMenuCatContentVM AboutUs { get; set; }
        public ContactUsVM ContactUs { get; set; }
    }
}
