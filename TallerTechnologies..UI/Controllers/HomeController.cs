using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TallerTechnologies.Domain.Models;
using TallerTechnologies.Domain.Services.Interfaces;

namespace TallerTechnologies.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarsService _service;

        public HomeController(ILogger<HomeController> logger, ICarsService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            List<CarViewModel> cars = _service.GetAll();
            return View(cars);
        }

        //[HttpPost, ActionName("GamePlay")]
        public IActionResult GamePlay(int id, double price)
        {
            bool retValue = false;
            var car = _service.GetOne(id);
            var cars = _service.GetAll();
            string sPrice = Request.Form["txtPrice" + id.ToString()];
            price = double.Parse(Request.Form["txtPrice" + id.ToString()]);

            //Initialize number of tries
            int? tries = HttpContext.Session.GetInt32("Tries");
            if (tries == null)
            {
                tries = 0;
                HttpContext.Session.SetInt32("Tries", tries.Value);
            }
            tries++;
            
            //Verify wining conditions
            if (car != null && car.Price == price && tries <= 5000)
            {
                ViewData["SucessText"] = "Great job!";
                tries = 0;
                retValue = true;
            }
            HttpContext.Session.SetInt32("Tries", tries.Value);
            return View("Index", cars);
            //return RedirectToAction("Index");
        }
        public IActionResult Privacy()
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
