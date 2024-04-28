
using ManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var session = HttpContext.Session.GetString("userSession");

            if (!string.IsNullOrEmpty(session))
            {
                ModelUser modelUser = JsonConvert.DeserializeObject<ModelUser>(session);
                return View(modelUser);
            }

            return RedirectToAction("Index", "Login"); ;
        }
        public IActionResult Privacy()
        {
            return View();
        }

    }
}