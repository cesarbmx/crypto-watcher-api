using CesarBmx.CryptoWatcher.Api2.ViewModelBuilders;
using CesarBmx.CryptoWatcher.Api2.ViewModels;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.CryptoWatcher.Application.Settings;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CesarBmx.CryptoWatcher.Api2.Pages.Charts
{
    public class IndexModel : PageModel
    {
        private readonly ChartService _chartService;
        private readonly AppSettings _appSettings;

        public List<Chart>? Charts { get; set; }

        public IndexModel(ChartService chartService, AppSettings appSettings)
        {
            _chartService = chartService;
            _appSettings = appSettings;
        }

        public async Task OnGetAsync()
        {
            // Get response
            var chartResponse = await _chartService.GetCharts(_appSettings.LineRetention);

            // Build
            Charts = ChartBuilder.BuildCharts(chartResponse);
        }
    }
}
