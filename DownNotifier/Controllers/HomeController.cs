using DownNotifier.Models;
using DownNotifierData.Repositories;
using DownNotifierEntities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DownNotifier.Controllers
{
    [UserFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IParameterRepository _parameterRepository;

        public HomeController(ILogger<HomeController> logger, IParameterRepository parameterRepository)
        {
            _logger = logger;
            _parameterRepository = parameterRepository; 
        }

        public IActionResult Index()
        {
            List<Parameter> parameters = _parameterRepository.ParameterList();
            return View(parameters);
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