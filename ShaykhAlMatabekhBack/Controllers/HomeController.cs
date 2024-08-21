using BackEgyVision.Infrastructure;
using BackEgyVision.Models;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionCore.Infrastructure;
using EgyVisionService.EgyVision;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace BackEgyVision.Controllers
{
    [SessionExpireFilter]
    [Serializable]
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            try
            {
                var aboutUsService = new LKMenuCatContentService();
                var model = aboutUsService.Search(new LKMenuCatContentVM() { MenuCatId = 1 }).FirstOrDefault();
                return View(model);
            }
            catch (Exception)
            {
                return View(new LKMenuCatContentVM());
            }
        }
        [HttpPost]
        public IActionResult UpdateAboutUs(LKMenuCatContentVM vm)
        {
            try
            {
                var service = new LKMenuCatContentService();

                if (vm.ID > 0)
                {
                    service.Update(vm);
                }
                else
                {
                    vm.MenuCatId = 1; // about us cat
                    service.Insert(vm);
                }
                return RedirectToAction("AboutUs");
            }
            catch (Exception)
            {
                return RedirectToAction("AboutUs");
            }
        }
        public IActionResult Mission()
        {
            try
            {
                var missionService = new LKMenuCatContentService();
                var model = missionService.Search(new LKMenuCatContentVM() { MenuCatId = 2 }).FirstOrDefault();
                return View(model);
            }
            catch (Exception)
            {
                return View(new LKMenuCatContentVM());
            }
        }
        public IActionResult UpdateMission(LKMenuCatContentVM vm)
        {
            try
            {
                var service = new LKMenuCatContentService();
                if (vm.ID > 0)
                {
                    service.Update(vm);
                }
                else
                {
                    vm.MenuCatId = 2;   // mission cat
                    service.Insert(vm);
                }
                return RedirectToAction("Mission");
            }
            catch (Exception)
            {
                return RedirectToAction("Mission");
            }
        }
        public IActionResult Vision()
        {
            try
            {
                var vissionService = new LKMenuCatContentService();
                var model = vissionService.Search(new LKMenuCatContentVM() { MenuCatId = 3 }).FirstOrDefault();
                return View(model);
            }
            catch (Exception)
            {
                return View(new LKMenuCatContentVM());
            }
        }
        public IActionResult UpdateVision(LKMenuCatContentVM vm)
        {
            try
            {
                var service = new LKMenuCatContentService();

                if (vm.ID > 0)
                {
                    service.Update(vm);
                }
                else
                {
                    vm.MenuCatId = 3; // vission cat
                    service.Insert(vm);
                }
                return RedirectToAction("Vision");
            }
            catch (Exception)
            {
                return RedirectToAction("Vision");
            }
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            try
            {
                var contactUs = new ContactUsService();
                var model = contactUs.Search(new ContactUsVM()).FirstOrDefault();
                return View(model);
            }
            catch (Exception)
            {
                return View(new ContactUsVM());
            }
        }
        [HttpPost]
        public IActionResult UpdateContactUs(ContactUsVM vm)
        {
            try
            {
                var contactUsService = new ContactUsService();
                if (vm.ID > 0)
                {
                    contactUsService.Update(vm);
                }
                else
                {
                    contactUsService.Insert(vm);
                }
                return RedirectToAction("ContactUs");
            }
            catch (Exception)
            {
                return RedirectToAction("ContactUs");
            }
        }
        public IActionResult Social()
        {
            try
            {
                var contactUsService = new ContactUsService();
                var model = contactUsService.Search(new ContactUsVM()).FirstOrDefault();
                return View(model);
            }
            catch (Exception)
            {
                return View(new ContactUsVM());
            }
        }
        [HttpPost]
        public IActionResult UpdateSocial(ContactUsVM vm)
        {
            try
            {
                if (vm.ID > 0)
                {
                    var contactUsService = new ContactUsService();
                    contactUsService.Update(vm);
                }
                return RedirectToAction("Social");
            }
            catch (Exception)
            {
                return RedirectToAction("Social");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public JsonResult EditLogo()
        {
            try
            {
                IFormFileCollection LogoImage = Request.Form.Files;
                // Prepare The Attachment file
                var files = LogoImage;
                string fName = "";
                byte[] fileData = null;
                // update attahment
                IAttachmentsService attachmentsService = new AttachmentsService();
                if (files.Count > 0)
                {
                    var oldAtts = attachmentsService.Search(new AttachmentsVM()
                    {
                        LKKeyTypeId = 6, // colored logo file
                        LKAttachmentTypeId = 3,
                        KeyId = 99999   // static value for logo indeex
                    });
                    var oldAtt = attachmentsService.Search(new AttachmentsVM()
                    {
                        LKKeyTypeId = 6, // colored logo file
                        LKAttachmentTypeId = 3,
                        KeyId = 99999   // static value for logo indeex
                    }).FirstOrDefault();
                    if (oldAtt != null)
                        attachmentsService.Delete(oldAtt);
                }
                foreach (IFormFile source in files)
                {
                    fName = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim().ToString().Replace("\"", "");
                    fName = Helper.EnsureCorrectFilename(fName);
                    fileData = null;
                    using (var binaryReader = new BinaryReader(source.OpenReadStream()))
                    {
                        fileData = binaryReader.ReadBytes((int)source.Length);
                    }
                    AttachmentsVM att = new AttachmentsVM();
                    att.AttachmentName = fName;
                    att.AttachmentContent = source.ContentType;
                    att.AttachmentFile = fileData;
                    att.UploadedDate = DateTime.Now;
                    att.LKKeyTypeId = 6;
                    att.LKAttachmentTypeId = 3;
                    att.KeyId = 99999;
                    if (att.AttachmentFile != null && att.AttachmentFile.Length > 0)
                        attachmentsService.Insert(att);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult LoadLogoAttachments()
        {
            try
            {
                AttachmentsService attachmentsService = new AttachmentsService();
                var atts = attachmentsService.Search(new AttachmentsVM()
                {
                    KeyId = 99999,
                    LKKeyTypeId = 6,
                    LKAttachmentTypeId = 3
                }).FirstOrDefault();
                return Json(new { logo = atts });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
