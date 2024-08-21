using BackEgyVision.Infrastructure;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionCore.Infrastructure;
using EgyVisionService.EgyVision;
using EgyVisionService.HelperServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Mvc;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using FileContentResult = Microsoft.AspNetCore.Mvc.FileContentResult;
using FileResult = Microsoft.AspNetCore.Mvc.FileResult;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace BackEgyVision.Controllers
{
    [SessionExpireFilter]
    [Serializable]
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class ProjectsController : Microsoft.AspNetCore.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoadProjects()
        {
            try
            {
                ProjectsService slidersService = new ProjectsService();
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                int start = int.Parse(HttpContext.Request.Form["start"].FirstOrDefault());
                int length = int.Parse(HttpContext.Request.Form["length"].FirstOrDefault());

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();


                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                ProjectsVM model = new ProjectsVM();
                model.jtPageSize = length;
                model.jtStartIndex = start;
                model.jtSorting = sortColumn + " " + sortColumnDirection;
                var projectsData = slidersService.Search(model);

                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = model.TotalRecordCount, recordsTotal = model.TotalRecordCount, data = projectsData });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public FileResult DownProjectImage(long projectId)
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
        public AttachmentsVM DownAtt(long Id)
        {
            try
            {
                IAttachmentsService AttachmentsServ = new AttachmentsService();
                AttachmentsVM model = new AttachmentsVM();
                model.KeyId = Id;
                model.LKKeyTypeId = 3;   // Cover Picture For Project
                model.LKAttachmentTypeId = 2;      // Projects
                model = AttachmentsServ.Search(model).FirstOrDefault();
                return model;
            }
            catch
            {
                return null;
            }
        }
        public JsonResult DeleteProject(long projectId)
        {
            try
            {
                IProjectsService projectsService = new ProjectsService();
                var model = projectsService.GetById(projectId);
                // delete the project
                projectsService.Delete(model);
                // delete the attachments
                DeleteProjectAttatchment(projectId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error" });
            }
        }
        private void DeleteProjectAttatchment(long projectId)
        {
            IAttachmentsService attachmentsService = new AttachmentsService();
            AttachmentsVM attachmentsVM = new AttachmentsVM();
            //delete cover picture
            attachmentsVM.KeyId = projectId;
            attachmentsVM.LKKeyTypeId = 3;
            attachmentsVM.LKAttachmentTypeId = 2;
            List<AttachmentsVM> attachmentsList = attachmentsService.Search(attachmentsVM);
            if (attachmentsList.Count != 0)
            {
                attachmentsService.Delete(attachmentsList[0]);
            }
            //delete gallery pictures
            attachmentsVM.KeyId = projectId;
            attachmentsVM.LKKeyTypeId = 4;
            attachmentsVM.LKAttachmentTypeId = 2;
            attachmentsList = attachmentsService.Search(attachmentsVM);
            attachmentsList.ForEach(elemnt =>
            {
                attachmentsService.Delete(elemnt);
            });
        }
        public IActionResult ProjectDetails(long projectId)
        {
            try
            {
                if (projectId > 0)
                {
                    ProjectsService slidersService = new ProjectsService();
                    var model = slidersService.GetById(projectId);
                    return View(model);
                }
                else
                    return View();

            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult UpdateProject(ProjectsVM projectsVM)
        {
            try
            {
                IProjectsService projectsService = new ProjectsService();
                IAttachmentsService AttachmentServ = new AttachmentsService();
                // update project
                if (projectsVM.ProjectId > 0)
                {
                    var tryUpdate = projectsService.Update(projectsVM);
                    if (tryUpdate)
                    {
                        var files = Request.Form.Files;
                        string fName = "";
                        byte[] fileData = null;
                        foreach (IFormFile source in files)
                        {
                            AttachmentsVM att = new AttachmentsVM();
                            fName = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim().ToString().Replace("\"", "");
                            fName = MISC.EnsureCorrectFilename(fName);
                            fileData = null;
                            using (var binaryReader = new BinaryReader(source.OpenReadStream()))
                            {
                                fileData = binaryReader.ReadBytes((int)source.Length);
                            }
                            att.AttachmentName = fName;
                            att.AttachmentContent = source.ContentType;
                            att.AttachmentFile = fileData;
                            att.UploadedDate = DateTime.Now;
                            att.KeyId = projectsVM.ProjectId;
                            if (source.Name == "CoverFile")
                            {
                                att.LKKeyTypeId = 3;
                                att.LKAttachmentTypeId = 2;
                                // delete cover image
                                var modelToDeleteOfAtt = AttachmentServ.Search(new AttachmentsVM()
                                {
                                    LKKeyTypeId = 3,
                                    LKAttachmentTypeId = 2,
                                    KeyId = projectsVM.ProjectId

                                }).FirstOrDefault();
                                if (modelToDeleteOfAtt != null)
                                    AttachmentServ.Delete(modelToDeleteOfAtt);

                            }
                            if (source.Name == "GalleryFiles")
                            {
                                att.LKKeyTypeId = 4;
                                att.LKAttachmentTypeId = 2;
                            }
                            AttachmentServ.Insert(att);
                        }
                    }
                    return Json(new { Result = "OK" });
                }
                // Created project
                else
                {
                    var insertedModel = projectsService.InsertAndReturn(projectsVM);
                    if (insertedModel != null)
                    {
                        var files = Request.Form.Files;
                        string fName = "";
                        byte[] fileData = null;
                        foreach (IFormFile source in files)
                        {
                            AttachmentsVM att = new AttachmentsVM();
                            fName = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim().ToString().Replace("\"", "");
                            fName = MISC.EnsureCorrectFilename(fName);
                            fileData = null;
                            using (var binaryReader = new BinaryReader(source.OpenReadStream()))
                            {
                                fileData = binaryReader.ReadBytes((int)source.Length);
                            }
                            att.AttachmentName = fName;
                            att.AttachmentContent = source.ContentType;
                            att.AttachmentFile = fileData;
                            att.UploadedDate = DateTime.Now;
                            att.KeyId = insertedModel.ProjectId;
                            if (source.Name == "CoverFile")
                            {
                                att.LKKeyTypeId = 3;
                                att.LKAttachmentTypeId = 2;
                            }
                            if (source.Name == "GalleryFiles")
                            {
                                att.LKKeyTypeId = 4;
                                att.LKAttachmentTypeId = 2;
                            }
                            AttachmentServ.Insert(att);
                        }
                    }
                    //return RedirectToAction("");
                    return Json(new { Result = "OK" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        
        public JsonResult LoadProjectAttachments(int keyTypeId, long projectId)
        {
            try
            {
                AttachmentsService attachmentsService = new AttachmentsService();
                var atts = attachmentsService.Search(new AttachmentsVM()
                {
                    KeyId = projectId,
                    LKKeyTypeId = keyTypeId,
                    LKAttachmentTypeId = 2
                });
                return Json(new { images = atts });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public JsonResult DeleteAttachment(long attachmentId)
        {
            try
            {
                AttachmentsService attachmentsService = new AttachmentsService();
                if (attachmentsService.Delete(attachmentsService.GetById(attachmentId)))
                    return Json(new { result = "OK" });
                else
                    return Json(new { result = "Error" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "Error" });
            }
        }
    }
}
