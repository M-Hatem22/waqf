using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using EgyVisionService.HelperServices;
using System.Net.Http.Headers;
using BackEgyVision.Infrastructure;
using Newtonsoft.Json.Linq;

namespace BackEgyVision.Controllers
{
    public class ToaaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult details()
        {
            return View();
        }

        public IActionResult LoadToaaFiles()
        {
            try
            {
                IToaaFilesService service = new ToaaFilesService();
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                ToaaFilesVM model = new ToaaFilesVM();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    model = JsonConvert.DeserializeObject<ToaaFilesVM>(searchValue);
                }
                int skip = start != null ? Convert.ToInt32(start) : 0;

                model.jtPageSize = pageSize;
                model.jtStartIndex = skip;
                model.jtSorting = sortColumn.Trim() + " " + sortColumnDirection;

                var att = service.Search(model);
                return Json(new { draw = draw, recordsFiltered = model.TotalRecordCount, recordsTotal = model.TotalRecordCount, data = att });
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                throw;
            }
        }
        public IActionResult basicDataFiles()
        {
            try
            {
                IToaaFilesService service = new ToaaFilesService();
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                ToaaFilesVM model = new ToaaFilesVM();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    model = JsonConvert.DeserializeObject<ToaaFilesVM>(searchValue);
                }
                int skip = start != null ? Convert.ToInt32(start) : 0;

                model.jtPageSize = pageSize;
                model.jtStartIndex = skip;
                model.jtSorting = sortColumn.Trim() + " " + sortColumnDirection;

                var att = service.GetBasicFiles(model);
                return Json(new { draw = draw, recordsFiltered = model.TotalRecordCount, recordsTotal = model.TotalRecordCount, data = att });
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                throw;
            }
        }
        public JsonResult basicData()
        {

            try
            {
                IToaaBasicDataService service = new ToaaBasicDataService();

                ToaaBasicDataVM model = new ToaaBasicDataVM();
                var att = service.Search(model);
                return Json(new { result = "OK", data = att });
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                throw;
            }
        }
        public IActionResult EditData(ToaaBasicDataVM model)
        {
            if(model == null) { return Json(new {Result = "Error", Message = "برجاء ادخال البيانات"});}
            try { 
            IToaaBasicDataService fileservice = new ToaaBasicDataService();
            var att = fileservice.Search(model);
            model.lastChange = DateTime.Now;
            if (fileservice.Update(model))
            {
                    return RedirectToAction("details");
            }
            else
            {
                return Json(new { Result = "ERROR", Message = "حدث خطأ اثناء دخول البيانات" });
            }
            }
            catch(Exception e) { return Json(new { Result = "ERROR", Message = e.Message }); }
        }
        public JsonResult GetToaaFileTypes()
        {
            ////ToaaFilesService filesService = new ToaaFilesService();
            ////List<ToaaFilesVM> filesTypes = filesService.Search(new ToaaFilesVM());
            //ToaaFilesVM.Add(new ToaaFilesVM()
            //{

            //});
            var fileTypes = new List<string> { "pdf", "jpg", "png" };


            return Json(new { Result="ok" , data = fileTypes});
            //return Json(new { Result = "OK", data = GovernanceVM });


        }
        public JsonResult CreateFiles(ToaaFilesVM model)
        {
            try
            {

                if (String.IsNullOrEmpty(model.fileNameAr))
                {
                    return Json(new { Result = "ERROR", Message = "من فضلك قم بادخال الإسم عربى" });
                }
                else
                {
                    IToaaFilesService fileservice = new ToaaFilesService();
                    var files = Request.Form.Files;
                    if (files.Count() > 0)
                    {
                        string fName = "";
                        byte[] fileData = null;

                        fName = ContentDispositionHeaderValue.Parse(files[0].ContentDisposition).FileName.Trim().ToString().Replace("\"", "");
                        fName = MISC.EnsureCorrectFilename(fName);
                        fileData = null;
                        using (var binaryReader = new BinaryReader(files[0].OpenReadStream()))
                        {
                            fileData = binaryReader.ReadBytes((int)files[0].Length);
                        }
                        if (fileData.Length > 20971520)
                        {
                            return Json(new { Result = "ERROR", Message = "لا يمكن ان يزيد حجم الملف عن 20 ميجا" });
                        }
                        model.fileType = files[0].ContentType;
                        model.fileContent = fileData;
                        model.fileNameEn = fName;
                    }
                    model.uploadDate = DateTime.Now;
                    if (fileservice.Insert(model))
                    {
                        return Json(new { Result = "OK" });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = "حدث خطأ اثناء دخول البيانات" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }
        public JsonResult EditFiles(ToaaFilesVM model)
        {
            try
            {

                if (String.IsNullOrEmpty(model.fileNameAr))
                {
                    return Json(new { Result = "ERROR", Message = "من فضلك قم بادخال الإسم عربى" });
                }
                else
                {
                    IToaaFilesService fileservice = new ToaaFilesService();
                    var att = fileservice.Search(model);
                    var files = Request.Form.Files;
                    if (files.Count() > 0)
                    {
                        string fName = "";
                        byte[] fileData = null;

                        fName = ContentDispositionHeaderValue.Parse(files[0].ContentDisposition).FileName.Trim().ToString().Replace("\"", "");
                        fName = MISC.EnsureCorrectFilename(fName);
                        fileData = null;
                        using (var binaryReader = new BinaryReader(files[0].OpenReadStream()))
                        {
                            fileData = binaryReader.ReadBytes((int)files[0].Length);
                        }
                        if (fileData.Length > 20971520)
                        {
                            return Json(new { Result = "ERROR", Message = "لا يمكن ان يزيد حجم الملف عن 20 ميجا" });
                        }
                        model.fileType = files[0].ContentType;
                        model.fileContent = fileData;
                        model.fileNameEn = fName;
                    }
                    model.uploadDate = DateTime.Now;
                    if (fileservice.Update(model))
                    {
                        return Json(new { Result = "OK" });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = "حدث خطأ اثناء دخول البيانات" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }

        public JsonResult DeleteFile(int ID)
        {
            try
            {
                IToaaFilesService GovernanceAttachmentsService = new ToaaFilesService();
                ToaaFilesVM model = GovernanceAttachmentsService.GetById(ID);
                model.isDeleted = DateTime.Now;
                if (GovernanceAttachmentsService.Update(model))
                {

                    return Json(new { Result = "OK" });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "لا يمكن حذف هذا العنصر" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        public FileResult ShowFile(int AttId)
        {
            try
            {
                IToaaFilesService toaaFilesService = new ToaaFilesService();
                ToaaFilesVM att = new ToaaFilesVM();
                att.id = AttId;
                att = toaaFilesService.GetById(AttId);
                if (att != null)
                    return new FileContentResult(att.fileContent, att.fileType);
                else
                    return new NotFoundFileResult("image/jpeg");
            }
            catch
            {
                return new NotFoundFileResult("image/jpeg");
            }
        }
        [HttpGet]
        public FileResult DownFile(int AttId)
        {
            byte[] fileContent = null;
            string fileType = "";
            string file_Name = "";
            try
            {
                IToaaFilesService toaaFilesService = new ToaaFilesService();
                ToaaFilesVM att = new ToaaFilesVM();
                att.id = AttId;
                att = toaaFilesService.GetById(AttId);
                if (att != null)
                {
                    string[] subs = att.fileType.Split('/');
                    fileContent = (byte[])att.fileContent;
                    fileType = att.fileType;
                    file_Name = att.fileNameAr + "." + subs[1];
                    return File(fileContent, fileType, file_Name);
                }
                else
                {
                    return new NotFoundFileResult("image/jpeg");
                }
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                return File(fileContent, fileType, file_Name);
            }
        }
    }
}
