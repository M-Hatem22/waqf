using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using EgyvisionVS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyvisionVS.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        public SliderViewComponent()
        {

        }
        public IViewComponentResult Invoke()
        {
            try
            {
                SliderAttachmentViewService sliderAttachmentViewService = new SliderAttachmentViewService();
                var subSliders = sliderAttachmentViewService.Search(new SliderAttachmentViewVM()
                {
                    LKKeyTypeId = 1,
                    LKAttachmentTypeId = 2,
                    OrderBy = "SliderId"
                });
                var mainSliders = sliderAttachmentViewService.Search(new SliderAttachmentViewVM()
                {
                    LKKeyTypeId = 1,
                    LKAttachmentTypeId = 1 ,
                    OrderBy = "SliderId"
                });
                SliderComponentVM model = new SliderComponentVM();
                model.SubSliders = subSliders;
                model.MainSliders = mainSliders;
                return View(model);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
                                                                                                                
    }
}
