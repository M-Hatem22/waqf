using BackEgyVision.Infrastructure;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionCore.Infrastructure;
using EgyVisionService.EgyVision;
using EgyVisionService.HelperServices;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Drawing.Printing;

namespace BackEgyVision.Controllers
{
    public class GovernanceController : Controller
    {
        public IActionResult Governance()
        {
            return View();
        }
        public IActionResult LoadGovernanceAttachment()
        {
            try
            {
                IGovernanceAttachmentsService service = new GovernanceAttachmentsService();
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
                GovernanceAttachmentsVM model = new GovernanceAttachmentsVM();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    model = JsonConvert.DeserializeObject<GovernanceAttachmentsVM>(searchValue);
                }
                int skip = start != null ? Convert.ToInt32(start) : 0;

                model.jtPageSize = pageSize;
                model.jtStartIndex = skip;
                model.jtSorting = sortColumn.Trim() + " " + sortColumnDirection;
              
                var att = service.GetwithoutAttachments(model);
                return Json(new { draw = draw, recordsFiltered = model.TotalRecordCount, recordsTotal = model.TotalRecordCount, data = att });
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                throw;
            }
        }

        public JsonResult GetGovernance()
        {
            ILkGovernanceService LkGovernanceService = new LkGovernanceService();                              
            List<LkGovernanceVM> GovernanceVM = LkGovernanceService.Search(new LkGovernanceVM());
            //GovernanceVM.Add(new LkGovernanceVM()
            //    {
            //        LkGovernanceId = -1,
            //        LkGovernanceNameAr = "غير محددة",
            //        LkGovernanceNameEn = "Not Determined"
            //    });
            return Json(new { Result = "OK", data = GovernanceVM });
           

        }

        public JsonResult ViewList(int fileid)
        {
            IFile_last_viewService service = new File_last_viewService();
            List<File_last_viewVM> vm = service.Getlastread(fileid);
            return Json(new {Result = "OK", data = vm });

        }
        public JsonResult CreateGovernance(GovernanceAttachmentsVM model)
        {
            try
            {
               
                if (String.IsNullOrEmpty(model.GovernanceAttachmentsNameAr))
                {
                    return Json(new { Result = "ERROR", Message = "من فضلك قم بادخال الإسم عربى" });
                }
                else
                {
                    IGovernanceAttachmentsService GovernanceAttachmentsService = new GovernanceAttachmentsService();
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
                        model.AttachmentContent = files[0].ContentType;
                        model.AttachmentFile = fileData;
                        model.AttachmentName = fName;
                    }                  
                    model.UploadedDate = DateTime.Now;
                    if (GovernanceAttachmentsService.Insert(model))
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
        public JsonResult CreateGovernanceType(LkGovernanceVM model)
        {
            try
            {

                if (String.IsNullOrEmpty(model.LkGovernanceNameAr))
                {
                    return Json(new { Result = "ERROR", Message = "من فضلك قم بادخال الإسم عربى" });
                }
                else
                {
                    ILkGovernanceService LkGovernanceService = new LkGovernanceService();
                    
                    if (LkGovernanceService.Insert(model))
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
        public JsonResult DeleteGovernancee(long ID)
        {
            try
            {
                IGovernanceAttachmentsService GovernanceAttachmentsService = new GovernanceAttachmentsService();
                GovernanceAttachmentsVM model = GovernanceAttachmentsService.GetById(ID);
                model.Deleted = DateTime.Now;
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
        public FileResult ShowGovernanceyAtt(long AttId)
        {
            try
            {
                
                IGovernanceAttachmentsService GovernanceAttachmentsService = new GovernanceAttachmentsService();
                IFile_last_viewService service = new File_last_viewService();
                GovernanceAttachmentsVM att = new GovernanceAttachmentsVM();
                att.GovernanceAttachmentsId = AttId;
                att = GovernanceAttachmentsService.Search(att).FirstOrDefault();
                if (att != null)
                {
                    File_last_viewVM vm = new File_last_viewVM();
                    vm.fileId = AttId;
                    vm.fileNameAr = att.GovernanceAttachmentsNameAr;
                    vm.userId = HttpContext.Session.GetString("UserId");
                    vm.userMail = HttpContext.Session.GetString("UserEmail");
                    vm.lastView = DateTime.Now;
                    service.Insert(vm);
                    att.AttachmentFile = readyPdf(att.AttachmentFile);
                    return new FileContentResult(att.AttachmentFile, att.AttachmentContent);
                }
                else
                    return new NotFoundFileResult("image/jpeg");
            }
            catch
            {
                return new NotFoundFileResult("image/jpeg");
            }
        }
        [HttpGet]
        public FileResult DownGovernanceAtt(long AttId)
        {
            var userId = HttpContext.Session.GetString("UserId");
            byte[] fileContent = null;
            string fileType = "";
            string file_Name = "";
            try
            {
                IGovernanceAttachmentsService GovernanceAttachmentsService = new GovernanceAttachmentsService();
                IFile_last_viewService service = new File_last_viewService();

                GovernanceAttachmentsVM att = new GovernanceAttachmentsVM();
                att.GovernanceAttachmentsId = AttId;
                att = GovernanceAttachmentsService.Search(att).FirstOrDefault();
                if (att != null)
                {
                    File_last_viewVM vm = new File_last_viewVM();
                    vm.fileId = AttId;
                    vm.fileNameAr = att.GovernanceAttachmentsNameAr;
                    vm.userId = HttpContext.Session.GetString("UserId");
                    vm.userMail = HttpContext.Session.GetString("UserEmail");
                    vm.lastDownload = DateTime.Now;
                    service.Insert(vm);

                    string[] subs = att.AttachmentContent.Split('/');
                    fileContent = readyPdf(att.AttachmentFile);
                    fileType = att.AttachmentContent;
                    file_Name = att.AttachmentName + "." + subs[1];
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
        private byte[] readyPdf(byte[] file)
        {
            byte[] inputPdf = file;
            string watermarkText = HttpContext.Session.GetString("UserFullName");

            byte[] outputPdf = AddWatermarkToPdf(inputPdf, watermarkText);

            return outputPdf;
        }
        static byte[] AddWatermarkToPdf(byte[] inputFile, string watermarkText)
        {
            using (MemoryStream inputStream = new MemoryStream(inputFile))
            using (MemoryStream outputStream = new MemoryStream())
            {
                PdfReader pdfReader = new PdfReader(inputStream);
                PdfStamper stamper = new PdfStamper(pdfReader, outputStream);
                int pageCount = pdfReader.NumberOfPages;

                for (int i = 1; i <= pageCount; i++)
                {
                    PdfContentByte overContent = stamper.GetOverContent(i);
                    AddWatermark(overContent, watermarkText, pdfReader.GetPageSize(i));
                }

                stamper.Close();
                pdfReader.Close();

                return outputStream.ToArray();
            }
        }

        static void AddWatermark(PdfContentByte overContent, string watermarkText , Rectangle pageSize)
        {
            //float fontSize = 50;
            //float xPosition = 300;
            //float yPosition = 400;
            //float angle = 45;

            //BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
            //PdfGState gstate = new PdfGState
            //{
            //    FillOpacity = 0.8f
            //};

            //overContent.SaveState();
            //overContent.SetGState(gstate);
            //overContent.SetColorFill(BaseColor.GRAY);
            //overContent.BeginText();
            //overContent.SetFontAndSize(baseFont, fontSize);
            //overContent.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, xPosition, yPosition, angle);
            //overContent.EndText();
            //overContent.RestoreState();

            float fontSize = 70;
            float angle = 35;

            float topYPosition = pageSize.Top - 70;
            float middleYPosition = pageSize.Height / 2;
            float bottomYPosition = 50;

            float leftXPosition = pageSize.Width / 4;
            float rightXPosition = (pageSize.Width / 4) * 3;
            float centerXPosition = pageSize.Width / 2;

            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
            PdfGState gstate = new PdfGState
            {
                FillOpacity = 0.9f
            };

            overContent.SaveState();
            overContent.SetGState(gstate);
            overContent.SetColorFill(BaseColor.GRAY);

            // Top Watermark
            DrawWatermark(overContent, watermarkText, centerXPosition, topYPosition, angle, baseFont, fontSize);

            // Middle Left Watermark
            DrawWatermark(overContent, watermarkText, leftXPosition, middleYPosition, angle, baseFont, fontSize);

            // Middle Right Watermark
            DrawWatermark(overContent, watermarkText, rightXPosition, middleYPosition, angle, baseFont, fontSize);

            // Bottom Watermark
            DrawWatermark(overContent, watermarkText, centerXPosition, bottomYPosition, angle, baseFont, fontSize);

            overContent.RestoreState();
        }
        static void DrawWatermark(PdfContentByte overContent, string watermarkText, float xPosition, float yPosition, float angle, BaseFont baseFont, float fontSize)
        {
            overContent.BeginText();
            overContent.SetFontAndSize(baseFont, fontSize);
            overContent.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, xPosition, yPosition, angle);
            overContent.EndText();
        }
    }
}
