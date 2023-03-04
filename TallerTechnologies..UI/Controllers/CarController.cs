using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TallerTechnologies.Domain.Models;
using TallerTechnologies.Domain.Services.Interfaces;

namespace TallerTechnologies.UI.Controllers
{
    public class CarController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarsService _service;

        public CarController(ILogger<HomeController> logger, ICarsService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            var cars = _service.GetAll();
            return View(cars);
        }

        [HttpPost, ActionName("Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var car = _service.GetOne(id);
            return View(car);
        }

        [HttpPost, ActionName("Add")]
        public IActionResult Add(int id)
        {
            var car = new CarViewModel();
            return View(car);
        }

        [HttpPost, ActionName("Update")]
        public IActionResult Update(CarViewModel car)
        {
            var newCar = _service.Update(car);
            return RedirectToAction("Detail", new { id = newCar.Id });
        }

        [HttpPost, ActionName("Save")]
        public IActionResult Save(CarViewModel car)
        {
            if (car.Id ==0)
            {
                var newCar = _service.Insert(car);
            }
            else
            {
                var newCar = _service.Update(car);
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Add()
        {
            //var car = _service.GetOne(id);
            return View();
        }

        public IActionResult Detail(int id)
        {
            var car = _service.GetOne(id);
            return View(car);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
