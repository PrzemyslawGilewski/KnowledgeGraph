using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KnowledgeGraph.Web.Models;
using MediatR;
using AutoMapper;
using KnowledgeGraph.Application.Request;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace KnowledgeGraph.Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("/Home/Error/404")]
        public IActionResult Error404()
        {
            return View();
        }

        [Route("/Home/Error/{code:int}")]
        public IActionResult Error(string code)
        {
            if (code == "404")
            {
                return View("Error404");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguageSimple(string culture, string returnUrl)
        {
            if (culture.ToLower() == "pl")
            {
                culture = "en";
            }
            else
            {
                culture = "pl";
            }

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) });

            return LocalRedirect(returnUrl);
        }
    }
}
