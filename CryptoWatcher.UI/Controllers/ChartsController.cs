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
        private readonly CurrencyService _currencyService;
        private readonly IndicatorService _indicatorService;
        private readonly LineService _linesService;

        public ChartsController(
            CurrencyService currencyService,
            IndicatorService indicatorService,
            LineService linesService)
        {
            _currencyService = currencyService;
            _indicatorService = indicatorService;
            _linesService = linesService;
        }

        public async Task<IActionResult> Index()
        {
            // Get all currrencies
            var currencies = await _currencyService.GetAllCurrencies();
                
            // Get all indicators
            var indicators = await _indicatorService.GetAllIndicators("master");

            // Get all lines
            var lines = await _linesService.GetAllLines();

           // ViewModel
            var chartViewModel = ChartBuilder.BuildChartViewModel(currencies, indicators, lines);

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
