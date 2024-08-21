using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using EgyVisionService.HelperServices;
using EgyvisionVS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EgyvisionVS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var aboutUsService = new LKMenuCatContentService();
                var model = aboutUsService.Search(new LKMenuCatContentVM() { MenuCatId = 1 }).FirstOrDefault();
                IHomeAboutUsService service = new HomeAboutUsService();
                HomeAboutUsVM HomeAboutUsVM = service.Search(new HomeAboutUsVM()).FirstOrDefault();
                //model.ContentEn += model.TitleEn;
                //model.ContentEn += "</div></span></div></div>";
                model.HomeAboutUEn = HomeAboutUsVM != null? HomeAboutUsVM.HomeAboutUEn: "";
                model.HomeAboutUsAr = HomeAboutUsVM != null?  HomeAboutUsVM.HomeAboutUsAr: "";
                return View(model);
            }
            catch (Exception)
            {
                return View(new LKMenuCatContentVM() );
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Projects()
        {
            try
            {
                IProjectCoverAttachmentViewService projectservice = new ProjectCoverAttachmentViewService();
                var Projects = projectservice.Search(new ProjectCoverAttachmentViewVM() { });
                return View(Projects);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult AlmazaBay()
        {
            return View();
        }

        public IActionResult TwinTowers()
        {
            return View();
        }

        public IActionResult ZayedDunes()
        {
            return View();
        }

        public IActionResult Complex()
        {
            return View();
        }

        public IActionResult Regency()
        {
            return View();
        }
        public IActionResult About()
        {
            var aboutUsService = new LKMenuCatContentService();
            var AboutUs = aboutUsService.Search(new LKMenuCatContentVM() { MenuCatId = 1 }).FirstOrDefault();
            //AboutUs.ContentEn += AboutUs.TitleEn;
            //AboutUs.ContentEn += "</div></span></div></div>";
            var mistion = aboutUsService.Search(new LKMenuCatContentVM() { MenuCatId = 2 }).FirstOrDefault();
            return View(new List<LKMenuCatContentVM>() { AboutUs, mistion });
        }

        public IActionResult Contact()
        {
            var contactUsService = new ContactUsService();
            var ContactUs = contactUsService.Search(new ContactUsVM()).FirstOrDefault();
            return View(ContactUs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // back end ------------------
        // projects cover
        public JsonResult LoadProject()
        {
            try
            {
                IProjectsService attachmentsService = new ProjectsService();
                var Projects = attachmentsService.Search(new ProjectsVM()
                {
                });
                return Json(new { Projects = Projects });
            }
            catch (Exception ex)
            {
                return Json(new { Projects = new List<ProjectCoverAttachmentViewVM>() });
            }
        }
        public FileResult DownProjectCoverAttachment(int projectId)
        {
            try
            {
                AttachmentsVM att = DownAtt(projectId);
                if (att != null)
                    return new FileContentResult(att.AttachmentFile, att.AttachmentContent);
                else
                    return new NotFoundFileResult("image/jpeg");
            }
            catch
            {
                return new NotFoundFileResult("image/jpeg");
            }
        }
        public AttachmentsVM DownAtt(int Id)
        {
            try
            {
                IAttachmentsService AttachmentsServ = new AttachmentsService();
                AttachmentsVM model = new AttachmentsVM();
                model.KeyId = Id;
                model.LKKeyTypeId = 3;
                model.LKAttachmentTypeId = 2;
                model = AttachmentsServ.Search(model).FirstOrDefault();
                return model;
            }
            catch
            {
                return null;
            }
        }
        public IActionResult ProjectDetails(long projectId)
        {
            try
            {
                IProjectsService projectsService = new ProjectsService();
                var project = projectsService.GetById(projectId);
                //project.ContentEn += project.ProjectTitleEn;
                //project.ContentEn += "</div></span></div></div>";
                if (project != null)
                {
                    // load project attachment
                    IAttachmentsService attachmentsService = new AttachmentsService();
                    var images = attachmentsService.Search(new AttachmentsVM()
                    {
                        KeyId = projectId,
                        LKKeyTypeId = 4,
                        LKAttachmentTypeId = 2
                    });
                    project.GalleryPictures = new List<AttachmentsVM>();
                    project.GalleryPictures.AddRange(images);
                }
                return View(project);
            }
            catch (Exception ex)
            {
                return View(new ProjectsVM());
            }

        }
        public JsonResult LoadAboutUs()
        {
            try
            {
                var aboutUsService = new LKMenuCatContentService();
                var model = aboutUsService.Search(new LKMenuCatContentVM() { MenuCatId = 1 }).FirstOrDefault();
                return Json(new { AboutUsModel = model });
            }
            catch (Exception)
            {
                return Json(new { AboutUsModel = new LKMenuCatContentVM() });
            }
        }
        public JsonResult LoadProjectGallery(long projectId)
        {
            try
            {
                AttachmentsService attachmentsService = new AttachmentsService();
                var atts = attachmentsService.Search(new AttachmentsVM()
                {
                    KeyId = projectId,
                    LKKeyTypeId = 4,
                    LKAttachmentTypeId = 2
                });
                return Json(new { images = atts });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public FileResult DownLogoImage()
        {
            try
            {
                IAttachmentsService attachmentsService = new AttachmentsService();
                var logo = attachmentsService.Search(new AttachmentsVM()
                {
                    LKKeyTypeId = 6, // colored logo file
                    LKAttachmentTypeId = 3,
                    KeyId = 99999   // static value for logo index
                }).FirstOrDefault();
                if (logo != null)
                    return new FileContentResult(logo.AttachmentFile, logo.AttachmentContent);
                else
                    return new NotFoundFileResult("image/jpeg");
            }
            catch
            {
                return new NotFoundFileResult("image/jpeg");
            }
        }
        public JsonResult LoadContactUs()
        {
            try
            {
                var contactUsService = new ContactUsService();
                var model = contactUsService.Search(new ContactUsVM()).FirstOrDefault();
                return Json(new { contactUs = model });
            }
            catch (Exception)
            {
                return Json(new { contactUs = new ContactUsVM() });
            }
        }
    }
}

