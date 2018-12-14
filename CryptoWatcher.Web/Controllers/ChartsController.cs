using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using CryptoWatcher.Web.Models;
using MediatR;

namespace CryptoWatcher.Web.Controllers
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
            // Reponse
            var response = await _mediator.Send(new GetAllLinesRequest());

            var chartValue = string.Empty;
            var index = 0;
            foreach (var item in response)
            {
                item.Time = item.Time.AddMinutes(index);
                var dateTime = $"new Date({item.Time.Year},{item.Time.Month},{item.Time.Day},{item.Time.Hour},{item.Time.Minute})";
                chartValue += ", " + $"[{dateTime}, {item.Value}, {item.AverageBuy}, {item.AverageSell}]";
                index = index + new Random().Next(60,90);
            }

            if (!string.IsNullOrEmpty(chartValue)) chartValue = chartValue.Substring(2);

            // Return
            return View("Index", chartValue);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
