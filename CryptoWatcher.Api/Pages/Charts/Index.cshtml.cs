using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.ViewModelBuilders;
using CryptoWatcher.Api.ViewModels;
using CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoWatcher.Api.Pages.Charts
{
    public class IndexModel : PageModel
    {
        private readonly ChartService _chartService;

        public List<Chart> Charts { get; set; }

        public IndexModel(ChartService chartService)
        {
            _chartService = chartService;
        }

        public async Task OnGetAsync()
        {
            // Get response
            var chartResponse = await _chartService.GetAllCharts();

            // Build
            Charts = ChartBuilder.BuildCharts(chartResponse);
        }
    }
}
