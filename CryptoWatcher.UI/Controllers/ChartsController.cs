using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Application.Requests;
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
            // Get all currrencies
            var currencies = await _mediator.Send(new GetAllCurrenciesRequest());

            // Get all indicators
            var indicators = await _mediator.Send(new GetAllIndicatorsRequest{UserId = "master"});

            // Get all lines
            var lines = await _mediator.Send(new GetAllLinesRequest());

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
