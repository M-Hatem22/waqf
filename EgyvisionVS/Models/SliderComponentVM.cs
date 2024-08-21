using EgyVisionCore.Entities.EgyVision.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyvisionVS.Models
{
    public class SliderComponentVM
    {
        public SliderComponentVM()
        {
            SubSliders = new List<SliderAttachmentViewVM>();
            MainSliders = new List<SliderAttachmentViewVM>();
        }
        public List<SliderAttachmentViewVM> SubSliders { get; set; }
        public List<SliderAttachmentViewVM> MainSliders { get; set; }
    }
}
