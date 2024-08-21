using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SHC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SHC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(ILogger<HomeController> logger , IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult About()
        //{
        //    return View();
        //}
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult AlmahallaVillas()
        {
            return View();
        }
        public IActionResult AlBaeezPalace()
        {
            return View();
        }
        public IActionResult AbhaMall()
        {
            return View();
        }
        public IActionResult AlManeaRest()
        {
            return View();
        }
        public IActionResult BejmentFactory()
        {
            return View();
        }
        public IActionResult CoffeeshopsProject()
        {
            return View();
        }
        public IActionResult SpecializedMedicalCenterHospital()
        {
            return View();
        }
        public IActionResult WahetKhamees()
        {
            return View();
        }
        public IActionResult infustructure()
        {
            return View();
        }
        public IActionResult OtherWorkes()
        {
            return View();
        }
        public IActionResult Allprojects()
        {
            return View();
        }
        public IActionResult projects()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
