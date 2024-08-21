using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.RegularExpressions;

namespace EgyvisionVS.Models
{
    public class FooterViewComponent : ViewComponent
    {
        public FooterViewComponent()
        {

        }
        public IViewComponentResult Invoke()
        {
            try
            {
                FooterVM footerVM = new FooterVM();
                var aboutUsService = new LKMenuCatContentService();  
                var AboutUs = aboutUsService.Search(new LKMenuCatContentVM() { MenuCatId = 1 }).FirstOrDefault();
                //AboutUs.ContentEn += AboutUs.TitleEn;
                //AboutUs.ContentEn += "</div></span></div></div>";
                var contactUsService = new ContactUsService();
                var ContactUs = contactUsService.Search(new ContactUsVM()).FirstOrDefault();

                footerVM.AboutUs = AboutUs;
                footerVM.ContactUs = ContactUs;
                return View(footerVM);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
