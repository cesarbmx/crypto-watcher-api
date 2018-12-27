using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Application.Charts.Requests;
using CryptoWatcher.UI.Builders;
using Microsoft.AspNetCore.Mvc;
using CryptoWatcher.UI.Models;
using MediatR;

namespace CryptoWatcher.UI.Controllers
{
    public class ChartsController : Controller
    {
        private readonly IMediator _mediator;

        public ChartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            // Request
            var request = new GetAllChartsRequest();

            // Reponse
            var response = await _mediator.Send(request);

           // ViewModel
            var chartViewModel = ChartBuilder.BuildChartViewModel(response);

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
