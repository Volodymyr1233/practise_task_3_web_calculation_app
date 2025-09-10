using System.Diagnostics;
using CalculationApplicationWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculationApplicationWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var history = await CalculationRequest.GetHistory();
            ViewData["History"] = history;
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Index(int firstNumber, int secondNumber, string operation)
        {
            int calculationResult = await CalculationRequest.SendRequest(firstNumber, secondNumber, operation);
            var history = await CalculationRequest.GetHistory();
            ViewData["History"] = history;
            ViewData["Result"] = calculationResult;
            return View();
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
