using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Application.Services;
using CryptoWatcher.UI.Builders;
using Microsoft.AspNetCore.Mvc;
using CryptoWatcher.UI.Models;

namespace CryptoWatcher.UI.Controllers
{
    public class ChartsController : Controller
    {
        private readonly LineChartService _lineChartService;

        public ChartsController(LineChartService lineChartService)
        {
            _lineChartService = lineChartService;
        }

        public async Task<IActionResult> Index()
        {
            // Get all line charts
            var lineCharts = await _lineChartService.GetAllLineCharts();
                         
           // ViewModel
            var lineChartViewModel = LineChartBuilder.BuildLineChartViewModel(lineCharts);

            // Return
            return View("Index", lineChartViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
