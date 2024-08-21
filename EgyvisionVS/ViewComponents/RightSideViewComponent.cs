using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyvisionVS.ViewComponents
{
    public class RightSideViewComponent : ViewComponent
    {
        public RightSideViewComponent()
        {

        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var contactUsService = new ContactUsService();
                var ContactUs = contactUsService.Search(new ContactUsVM()).FirstOrDefault();
                return View(ContactUs);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
