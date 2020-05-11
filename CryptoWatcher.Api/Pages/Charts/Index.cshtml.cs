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
        private readonly LineChartService _lineChartService;

        public List<Chart> Charts { get; set; }

        public IndexModel(LineChartService lineChartService)
        {
            _lineChartService = lineChartService;
        }

        public async Task OnGetAsync()
        {
            // Get response
            var lineChartResponse = await _lineChartService.GetAllLineCharts();

            // Build
            Charts = ChartBuilder.BuildCharts(lineChartResponse);
        }
    }
}
