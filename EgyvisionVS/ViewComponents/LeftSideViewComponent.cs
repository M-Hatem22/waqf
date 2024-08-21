using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using EgyvisionVS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EgyvisionVS.ViewComponents
{
    public class LeftSideViewComponent : ViewComponent
    {
        public LeftSideViewComponent()
        {

        }
        public IViewComponentResult Invoke()
        {
            try
            {
                LeftSideModelVM leftSideVM = new LeftSideModelVM();

                var attachmentsService = new AttachmentsService();
                var logo = attachmentsService.Search(new AttachmentsVM() { 
                    KeyId = 99999,
                    LKKeyTypeId = 6,
                    LKAttachmentTypeId = 3

                }).FirstOrDefault();
                var contactUsService = new ContactUsService();
                var ContactUs = contactUsService.Search(new ContactUsVM()).FirstOrDefault();
                leftSideVM.LogoImage = logo;
                leftSideVM.ContactUs = ContactUs;
                return View(leftSideVM);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
