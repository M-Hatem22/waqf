using DinkToPdf;
using DinkToPdf.Contracts;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackEgyVision.Controllers
{
    [Serializable]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        IConverter _converter = null;

        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public string _cookieSelectedLanguage;

        public BaseController(IConverter converter)
        {
            _converter = converter;
            _httpContextAccessor = new HttpContextAccessor();
            var requestCulture = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();

            _cookieSelectedLanguage = requestCulture.RequestCulture.Culture.Name;
        }

        [Authorize]
        public FileResult pdfConvert(string c, string a, string f, string hc, string hu, string fc, string fu)
        {
            HeaderSettings header = new HeaderSettings();
            FooterSettings footer = new FooterSettings();
            if (!String.IsNullOrEmpty(hc) && !String.IsNullOrEmpty(hu))
                header = new HeaderSettings() { HtmUrl = $"https://{Request.Host}/Reports/" + hc + "/" + hu, Spacing = 4 };

            if (!String.IsNullOrEmpty(fc) && !String.IsNullOrEmpty(fu))
                footer = new FooterSettings() { HtmUrl = $"https://{Request.Host}/Reports/" + fc + "/" + fu, Spacing = 4 };
            else
                footer = new FooterSettings() { HtmUrl = $"https://{Request.Host}/Reports/footer.html", Spacing = 4 };

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings() { Top = 10 }, 
                
            },
                Objects = {
                    new ObjectSettings()
                    {
                        Page = $"https://{Request.Host}/"+c+"/"+a,
                    },
                }
            };

            byte[] pdf = _converter.Convert(doc);

            return File(pdf, "application/pdf", f);
        }

        [HttpPost]
        [Authorize]
        public byte[] pdfConvertHtmlContent(string html, string f, string hc, string hu, string fc, string fu, string ls)
        {
            HeaderSettings header = new HeaderSettings();
            FooterSettings footer = new FooterSettings();

            if (!String.IsNullOrEmpty(hc) && !String.IsNullOrEmpty(hu))
                header = new HeaderSettings() { HtmUrl = $"https://{Request.Host}/Reports/" + hc + "/" + hu, Spacing = 4 };

            if (!String.IsNullOrEmpty(fc) && !String.IsNullOrEmpty(fu))
                footer = new FooterSettings() { HtmUrl = $"https://{Request.Host}/Reports/" + fc + "/" + fu, Spacing = 4 };
            else if (!String.IsNullOrEmpty(fu))
                footer = new FooterSettings() { HtmUrl = $"https://{Request.Host}/Reports/" + fu, Spacing = 4 };
            else
                footer = new FooterSettings();

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation =ls.ToLower()=="landscape"? Orientation.Landscape :Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings() { Top = 10 }, 
                
                //Out = @"C:\DinkToPdf\src\DinkToPdf.TestThreadSafe\test.pdf",
            },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html,

                        HeaderSettings = header,
                        FooterSettings = footer
                    },
                }
            };

            byte[] pdf = _converter.Convert(doc);

            return pdf;
        }
        [HttpPost]
        [Authorize]
        public string pdfConvertHtmlContentToBase64(string html, string f, string hc, string hu, string fc, string fu, string ls)
        {
            HeaderSettings header = new HeaderSettings();
            FooterSettings footer = new FooterSettings();

            if (!String.IsNullOrEmpty(hc) && !String.IsNullOrEmpty(hu))
                header = new HeaderSettings() { HtmUrl = $"https://{Request.Host}/Reports/" + hc + "/" + hu, Spacing = 4 };

            if (!String.IsNullOrEmpty(fc) && !String.IsNullOrEmpty(fu))
                footer = new FooterSettings() { HtmUrl = $"https://{Request.Host}/Reports/" + fc + "/" + fu, Spacing = 4 };
            else if (!String.IsNullOrEmpty(fu))
                footer = new FooterSettings() { HtmUrl = $"https://{Request.Host}/Reports/" + fu, Spacing = 4 };
            else
                footer = new FooterSettings();

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation =ls.ToLower()=="landscape"? Orientation.Landscape :Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings() { Top = 10 }, 
                
                //Out = @"C:\DinkToPdf\src\DinkToPdf.TestThreadSafe\test.pdf",
            },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html,

                        HeaderSettings = header,
                        FooterSettings = footer
                    },
                }
            };

            byte[] pdf = _converter.Convert(doc);

            return System.Convert.ToBase64String(pdf);
        }
        public string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
        public SelectList GetEmptySelect()
        {
            SelectList selectList = null;
            List<LKCountriesVM> Items = new List<LKCountriesVM>();
            Items.Add(new LKCountriesVM()
            {
                LKCountryId = -1,
                LKCountryNameAr = "غير محددة",
                LKCountryNameEn = "Not Determined"
            });

            if (_cookieSelectedLanguage == "ar-SA")
                selectList = new SelectList(Items, "LKCountryId", "LKCountryNameAr");
            else if (_cookieSelectedLanguage == "en-US")
                selectList = new SelectList(Items, "LKCountryId", "LKCountryNameEn");
            else
                selectList = new SelectList(Items, "LKCountryId", "LKCountryNameEn");
            return selectList;
        }
        public SelectList GetCountrySelect(int selVal)
        {
            ILKCountriesService ILKCountriesServ = new LKCountriesService();
            SelectList selectList = null;
            LKCountriesVM filter = new LKCountriesVM();
            List<LKCountriesVM> Nationalities = ILKCountriesServ.Search(filter).ToList();

            Nationalities.Add(new LKCountriesVM()
            {
                LKCountryId = -1,
                LKCountryNameAr = "غير محددة",
                LKCountryNameEn = "Not Determined"
            });

            if (_cookieSelectedLanguage == "ar-SA")
                selectList = new SelectList(Nationalities, "LKCountryId", "LKCountryNameAr", selVal);
            else if (_cookieSelectedLanguage == "en-US")
                selectList = new SelectList(Nationalities, "LKCountryId", "LKCountryNameEn", selVal);
            else
                selectList = new SelectList(Nationalities, "LKCountryId", "LKCountryNameEn", selVal);
            return selectList;
        }
        public SelectList GetRegionSelect(int countryId, long selVal)
        {
            ILKRegionsService LKRegionsService = new LKRegionsService();
            SelectList selectList = null;

            List<LKRegionsVM> items = LKRegionsService.Search(new LKRegionsVM()
            {
                LKCountryId = countryId
            }).ToList();

            items.Add(new LKRegionsVM()
            {
                LKRegionId = -1,
                LKRegionNameAr = "غير محددة",
                LKRegionNameEn = "Not Determined"
            });

            if (_cookieSelectedLanguage == "ar-SA")
                selectList = new SelectList(items, "LKRegionId", "LKRegionNameAr", selVal);
            else if (_cookieSelectedLanguage == "en-US")
                selectList = new SelectList(items, "LKRegionId", "LKRegionNameEn", selVal);
            else
                selectList = new SelectList(items, "LKRegionId", "LKRegionNameEn", selVal);
            return selectList;
        }
        public JsonResult GetRegionsJson(int countryId)
        {
            ILKRegionsService LKRegionsService = new LKRegionsService();

            try
            {
                List<LKRegionsVM> items = LKRegionsService.Search(new LKRegionsVM()
                {
                    LKCountryId = countryId
                }).ToList();

                items.Add(new LKRegionsVM()
                {
                    LKRegionId = -1,
                    LKRegionNameAr = "غير محددة",
                    LKRegionNameEn = "Not Determined"
                });

                return Json(new { Result = "OK", items = items });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult GetCityJson(int regionId)
        {
            ILKCitiesService LKCitiesService = new LKCitiesService();

            try
            {
                List<LKCitiesVM> items = LKCitiesService.Search(new LKCitiesVM()
                {
                    LKRegionId = regionId
                }).ToList();

                items.Add(new LKCitiesVM()
                {
                    LKCityId = -1,
                    LKCityNameAr = "غير محددة",
                    LKCityNameEn = "Not Determined"
                });

                return Json(new { Result = "OK", items = items });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public SelectList GetCitySelect(long regionId, long selVal)
        {
            ILKCitiesService LKCitiesService = new LKCitiesService();
            SelectList selectList = null;

            List<LKCitiesVM> items = LKCitiesService.Search(new LKCitiesVM()
            {
                LKRegionId = regionId
            }).ToList();

            items.Add(new LKCitiesVM()
            {
                LKCityId = -1,
                LKCityNameAr = "غير محددة",
                LKCityNameEn = "Not Determined"
            });

            if (_cookieSelectedLanguage == "ar-SA")
                selectList = new SelectList(items, "LKCityId", "LKCityNameAr", selVal);
            else if (_cookieSelectedLanguage == "en-US")
                selectList = new SelectList(items, "LKCityId", "LKCityNameEn", selVal);
            else
                selectList = new SelectList(items, "LKCityId", "LKCityNameEn", selVal);
            return selectList;
        }
        public JsonResult GetCountryCode(int LKCountryId)
        {
            ILKCountriesService ILKCountriesServ = new LKCountriesService();
            LKCountriesVM countryVM = new LKCountriesVM();
            try
            {
                countryVM = ILKCountriesServ.GetById(LKCountryId);

                return Json(new { Result = "OK", CountryKey = countryVM.CountryKey });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }
        public SelectList GetCountryCodesSelect()
        {
            ILKCountriesService ILKCountriesServ = new LKCountriesService();
            SelectList selectList = null;
            List<LKCountriesVM> countryVM = new List<LKCountriesVM>();
            LKCountriesVM defaultCountryVM = new LKCountriesVM();

            try
            {
                int LKCountryId = 1;
                if (HttpContext.Session.GetInt32("LKCountryId") != null && HttpContext.Session.GetInt32("LKCountryId").Value > 0)
                    LKCountryId = HttpContext.Session.GetInt32("LKCountryId").Value;
                defaultCountryVM = ILKCountriesServ.GetById(LKCountryId);
                countryVM = ILKCountriesServ.Search(new LKCountriesVM());
                return selectList = new SelectList(countryVM.Select(x => x.CountryKey), defaultCountryVM.CountryKey);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
