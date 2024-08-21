using BackEgyVision.Infrastructure;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionCore.Infrastructure;
using EgyVisionService.EgyVision;
using EgyVisionService.HelperServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace BackEgyVision.Controllers
{
    [SessionExpireFilter]
    [Serializable]
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class SliderController : Controller
    {
        public IActionResult MainSlider()
        {
            return View();
        }
        public IActionResult SecondarySlider()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoadSliders()
        {
            try
            {
                ISlidersService slidersService = new SlidersService();
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

                SlidersVM model = new SlidersVM();
                model.jtPageSize = length;
                model.jtStartIndex = start;
                model.jtSorting = sortColumn + " " + sortColumnDirection;
                var slidersData = slidersService.Search(model);

                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = model.TotalRecordCount, recordsTotal = model.TotalRecordCount, data = slidersData });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public FileResult DownSliderImage(int sliderId)
        {
            try
            {

                AttachmentsVM att = DownAtt(sliderId);
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
                AttachmentsVM model = new AttachmentsVM();
                SlidersService slidersService = new SlidersService();
                var slider = slidersService.GetById(Id);
                if (slider != null)
                {
                    IAttachmentsService AttachmentsServ = new AttachmentsService();
                    model.KeyId = Id;
                    model.LKKeyTypeId = 1;
                    if (slider.MainSlider)
                        model.LKAttachmentTypeId = 1;
                    else
                        model.LKAttachmentTypeId = 2;
                    model = AttachmentsServ.Search(model).FirstOrDefault();
                }
                return model;
            }
            catch
            {
                return null;
            }
        }
        public JsonResult DeleteSlider(int sliderId)
        {
            try
            {
                ISlidersService slidersService = new SlidersService();
                SlidersVM model = slidersService.GetById(sliderId);
                slidersService.Delete(model);
                IAttachmentsService attachmentsService = new AttachmentsService();
                AttachmentsVM attachmentsVM = new AttachmentsVM();
                attachmentsVM.KeyId = sliderId;
                if (model.MainSlider)
                    attachmentsVM.LKAttachmentTypeId = 1;
                else
                    attachmentsVM.LKAttachmentTypeId = 2;
                attachmentsVM.LKKeyTypeId = 1;
                List<AttachmentsVM> attachmentsList = attachmentsService.Search(attachmentsVM);
                if (attachmentsList.Count != 0)
                {
                    attachmentsService.Delete(attachmentsList[0]);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error" });
            }
        }
        public JsonResult AddSlider(SlidersVM slidersVM)
        {
            try
            {
                ISlidersService slidersService = new SlidersService();
                var createdSlider = slidersService.InsertAndReturnModel(slidersVM);
                if (createdSlider != null)
                {
                    IFormFileCollection SliderImage = Request.Form.Files;
                    // Prepare The Attachment file
                    IAttachmentsService AttachmentsServ = new AttachmentsService();
                    AttachmentsVM att = new AttachmentsVM();
                    var files = SliderImage;
                    string fName = "";
                    byte[] fileData = null;
                    foreach (IFormFile source in files)
                    {
                        fName = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim().ToString().Replace("\"", "");
                        fName = Helper.EnsureCorrectFilename(fName);
                        fileData = null;
                        using (var binaryReader = new BinaryReader(source.OpenReadStream()))
                        {
                            fileData = binaryReader.ReadBytes((int)source.Length);
                        }
                        att.AttachmentName = fName;
                        att.AttachmentContent = source.ContentType;
                        att.AttachmentFile = fileData;
                        att.UploadedDate = DateTime.Now;
                        att.LKKeyTypeId = 1;
                        if (slidersVM.MainSlider)
                            att.LKAttachmentTypeId = 1;
                        else
                            att.LKAttachmentTypeId = 2;
                        att.KeyId = createdSlider.SliderId;
                        if (att.AttachmentFile != null && att.AttachmentFile.Length > 0)
                            AttachmentsServ.Insert(att);
                    }
                    return Json(new { Result = "OK" });
                }
                else
                    return Json(new { Result = "Error" });

            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetSliderById(int sliderId)
        {
            try
            {
                ISlidersService slidersService = new SlidersService();
                var slider = slidersService.GetById(sliderId);
                if (slider != null)
                {
                    IAttachmentsService AttachmentsServ = new AttachmentsService();
                    AttachmentsVM att = AttachmentsServ.Search(new AttachmentsVM()
                    {
                        LKKeyTypeId = 1,
                        LKAttachmentTypeId = slider.MainSlider ? 1 : 2,
                        KeyId = slider.SliderId
                    }).FirstOrDefault();
                    if (att != null)
                        slider.AttachmentFile = att.AttachmentFile;
                }
                return Json(new { Result = "OK", slider = slider });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public JsonResult EditSlider(SlidersVM slidersVM)
        {
            try
            {
                ISlidersService slidersService = new SlidersService();
                var TyrUpdate = slidersService.Update(slidersVM);
                if (TyrUpdate)
                {
                    IFormFileCollection SliderImage = Request.Form.Files;
                    // Prepare The Attachment file
                    var files = SliderImage;
                    string fName = "";
                    byte[] fileData = null;
                    // update attahment
                    IAttachmentsService attachmentsService = new AttachmentsService();
                    if (files.Count > 0)
                    {
                        if (slidersVM.MainSlider)
                        {
                            attachmentsService.Search(new AttachmentsVM()
                            {
                                LKKeyTypeId = 1,
                                LKAttachmentTypeId = 1,
                                KeyId = slidersVM.SliderId
                            }).ForEach(file => attachmentsService.Delete(file));
                        }
                        else if(!slidersVM.MainSlider)
                        {
                           attachmentsService.Search(new AttachmentsVM()
                            {
                                LKKeyTypeId = 1,
                                LKAttachmentTypeId = 2,
                                KeyId = slidersVM.SliderId
                            }).ForEach(file => attachmentsService.Delete(file));
                        }

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
                        att.LKKeyTypeId = 1;
                        if (slidersVM.MainSlider)
                            att.LKAttachmentTypeId = 1;
                        else
                            att.LKAttachmentTypeId = 2;
                        att.KeyId = slidersVM.SliderId;
                        if (att.AttachmentFile != null && att.AttachmentFile.Length > 0)
                            attachmentsService.Insert(att);
                    }
                    return Json(new { Result = "OK" });
                }
                else
                    return Json(new { Result = "Error" });

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
