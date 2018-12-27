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
        private readonly ChartService _chartService;

        public ChartsController(ChartService chartService)
        {
            _chartService = chartService;
        }

        public async Task<IActionResult> Index()
        {
            // Get all charts
            var charts = await _chartService.GetAllCharts();
                         
           // ViewModel
            var chartViewModel = ChartBuilder.BuildChartViewModel(charts);

            // Return
            return View("Index", chartViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
