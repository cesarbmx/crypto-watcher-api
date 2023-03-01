using CesarBmx.Notification.Api.ViewModelBuilders;
using CesarBmx.Notification.Api.ViewModels;
using CesarBmx.Notification.Application.Services;
using CesarBmx.Notification.Application.Settings;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CesarBmx.Notification.Api.Pages.Charts
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
