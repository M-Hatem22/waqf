using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Azure.WebJobs;
using System;

namespace EgyVisionService.HelperServices
{
    public interface IDinkToPdfPrintService
    {
        byte[] pdfConvertHtmlContentForApi(string html, string headerPath, string footerPath, bool isLandscape);
        byte[] pdfConvertHtmlContent(string html, string headerPath, string footerPath, bool isLandscape);
        string pdfConvertHtmlContentToBase64(string html, string headerPath, string footerPath, bool isLandscape);
        byte[] pdfConvertHtmlContentWithoutHeader(string html, bool isLandscape);
    }

    public class DinkToPdfPrintService : IDinkToPdfPrintService
    {
        IConverter _converter = null;
        public DinkToPdfPrintService(IConverter converter)
        {
            _converter = converter;
        }


        public byte[] pdfConvertHtmlContentWithoutHeader(string html, bool isLandscape)
        {
            try
            {
                var _converter = new SynchronizedConverter(new PdfTools());


                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = isLandscape==true ? Orientation.Landscape :Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = { Left = 0 , Right = 0 , Top= 0 , Bottom = 0 },
                
                //Out = @"C:\DinkToPdf\src\DinkToPdf.TestThreadSafe\test.pdf",
            },
                    Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8"}
                    },
                }
                };

                byte[] pdf = _converter.Convert(doc);

                _converter = null;

                return pdf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public byte[] pdfConvertHtmlContentForApi(string html, string headerPath, string footerPath, bool isLandscape)
        {
            try
            {
                //var _converter = new SynchronizedConverter(new PdfTools());
                HeaderSettings header = new HeaderSettings();
                FooterSettings footer = new FooterSettings();

                if (!String.IsNullOrEmpty(headerPath))
                    header = new HeaderSettings() { HtmUrl = headerPath, Spacing = 4 };
                else
                    header = new HeaderSettings();

                if (!String.IsNullOrEmpty(footerPath))
                    footer = new FooterSettings() { HtmUrl = footerPath, Spacing = 4 };
                else
                    footer = new FooterSettings();

                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = isLandscape==true ? Orientation.Landscape :Orientation.Portrait,
                PaperSize =  new PechkinPaperSize("2.8in", "15in"),//PaperKind.A4,
                Margins = { Left = 0 , Right = 0 , Top= 0 , Bottom = 0 },
                
                //Out = @"C:\DinkToPdf\src\DinkToPdf.TestThreadSafe\test.pdf",
            },
                    Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8"},
                        HeaderSettings = header,
                        FooterSettings = footer
                    },
                }
                };

                byte[] pdf = _converter.Convert(doc);
                //File.WriteAllBytes(@"G:\Saree3Tech\test2", pdf);
                return pdf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public byte[] pdfConvertHtmlContent(string html, string headerPath, string footerPath, bool isLandscape)
        {
            try
            {
                //var _converter = new SynchronizedConverter(new PdfTools());
                HeaderSettings header = new HeaderSettings();
                FooterSettings footer = new FooterSettings();

                if (!String.IsNullOrEmpty(headerPath))
                    header = new HeaderSettings() { HtmUrl = headerPath, Spacing = 4 };
                else
                    header = new HeaderSettings();

                if (!String.IsNullOrEmpty(footerPath))
                    footer = new FooterSettings() { HtmUrl = footerPath, Spacing = 4 };
                else
                    footer = new FooterSettings();

                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = isLandscape==true ? Orientation.Landscape :Orientation.Portrait,
                PaperSize =  PaperKind.A4,
                Margins = { Left = 0 , Right = 0 , Top= 0 , Bottom = 0 },
                
                //Out = @"C:\DinkToPdf\src\DinkToPdf.TestThreadSafe\test.pdf",
            },
                    Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8"},
                        HeaderSettings = header,
                        FooterSettings = footer
                    },
                }
                };

                byte[] pdf = _converter.Convert(doc);
                return pdf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //[Authorize]
        public string pdfConvertHtmlContentToBase64(string html, string headerPath, string footerPath, bool isLandscape)
        {
            try
            {
                var _converter = new SynchronizedConverter(new PdfTools());
                HeaderSettings header = new HeaderSettings();
                FooterSettings footer = new FooterSettings();

                if (!String.IsNullOrEmpty(headerPath))
                    header = new HeaderSettings() { HtmUrl = headerPath, Spacing = 4 };
                else
                    header = new HeaderSettings();

                if (!String.IsNullOrEmpty(footerPath))
                    footer = new FooterSettings() { HtmUrl = footerPath, Spacing = 4 };
                else
                    footer = new FooterSettings();

                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = isLandscape==true ? Orientation.Landscape :Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings() { Top = 10 }, 
                
                //Out = @"C:\DinkToPdf\src\DinkToPdf.TestThreadSafe\test.pdf",
            },
                    Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8"},
                        HeaderSettings = header,
                        FooterSettings = footer
                    },
                }
                };

                byte[] pdf = _converter.Convert(doc);

                return System.Convert.ToBase64String(pdf);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
