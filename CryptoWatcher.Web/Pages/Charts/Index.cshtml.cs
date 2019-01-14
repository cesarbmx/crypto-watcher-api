using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Application.Services;
using CryptoWatcher.Web.Builders;
using CryptoWatcher.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoWatcher.Web.Pages.Charts
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
