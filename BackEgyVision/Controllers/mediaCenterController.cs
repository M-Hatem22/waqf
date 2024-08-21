using BackEgyVision.Infrastructure;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using EgyVisionService.HelperServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;

namespace BackEgyVision.Controllers
{
    public class mediaCenterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult getAll()
        {

            //try
            //{
            //    ImediaCenterService service = new mediaCenterService();
            //    mediaCenterVM model = new mediaCenterVM();
            //    var att = service.Search(model);
            //    return Json(new { result = "OK", data = att });
            //}
            //catch (Exception ex)
            //{
            //    var Message = ex.Message;
            //    throw;
            //}

            try
            {
                ImediaCenterService service = new mediaCenterService();
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
                mediaCenterVM model = new mediaCenterVM();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    model = JsonConvert.DeserializeObject<mediaCenterVM>(searchValue);
                }
                int skip = start != null ? Convert.ToInt32(start) : 0;

                model.jtPageSize = pageSize;
                model.jtStartIndex = skip;
                model.jtSorting = sortColumn.Trim() + " " + sortColumnDirection;

                List<mediaCenterVM> att = service.Search(model);
                return Json(new { draw = draw, recordsFiltered = model.TotalRecordCount, recordsTotal = model.TotalRecordCount, data = att });
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                throw;
            }
        }

        public JsonResult CreateMedia(mediaCenterVM model)
        {
            try
            { 
                    ImediaCenterService fileservice = new mediaCenterService();
                var files = Request.Form.Files;
                //if (files.Count() > 0)
                //{
                //    string fName = "";
                byte[] fileData = null;

                //    fName = ContentDispositionHeaderValue.Parse(files[0].ContentDisposition).FileName.Trim().ToString().Replace("\"", "");
                //    fName = MISC.EnsureCorrectFilename(fName);
                //    fileData = null;
                using (var binaryReader = new BinaryReader(files[0].OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)files[0].Length);
                }
                if (fileData.Length > 20971520)
                {
                    return Json(new { Result = "ERROR", Message = "لا يمكن ان يزيد حجم الصورة عن 20 ميجا" });
                }
                //    //model.fileType = files[0].ContentType;
                model.image = fileData;
                //}
                model.dateAdded = DateTime.Now;
                    if (fileservice.Insert(model))
                    {
                        return Json(new { Result = "OK" });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = "حدث خطأ اثناء دخول البيانات" });
                    }
                
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }

        public JsonResult EditMedia(mediaCenterVM model)
        {
            try
            {
                ImediaCenterService mediaCenterService = new mediaCenterService();
                model.dateAdded = DateTime.Today;
                mediaCenterService.Update(model);
                return Json(new { result = "OK" });
            }catch(Exception e)
            {
                return Json(new { result = "Failed", message = e.Message });
            }

        }

        public JsonResult DeleteMedia (int  id)
        {
            try
            {
                ImediaCenterService mediaCenterService = new mediaCenterService();
                mediaCenterVM model = mediaCenterService.GetById(id);
                model.isDeleted = DateTime.Now;
                if (mediaCenterService.Update(model))
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
                return Json(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }

        }

        public FileResult getImage (int mediaId)
        {
            
            try
            {
                ImediaCenterService fileservice = new mediaCenterService();
                mediaCenterVM att = new mediaCenterVM();
                att = fileservice.GetById(mediaId);
                if (att != null)
                    return new FileContentResult(att.image, "image/jpg");
                else
                    return new NotFoundFileResult("image/jpeg");
            }
            catch
            {
                return new NotFoundFileResult("image/jpeg");
            }
        }
    }
}
