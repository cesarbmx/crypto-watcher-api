using System;
using System.ComponentModel.DataAnnotations;
using CryptoWatcher.Application.Watchers.Responses;
using MediatR;
using Newtonsoft.Json;

namespace CryptoWatcher.Application.Watchers.Requests
{
    public class UpdateWatcherRequest : IRequest<WatcherResponse>
    {
        [JsonIgnore] public Guid WatcherId { get; set; }
        [Required] public decimal Buy { get; set; }
        [Required] public decimal Sell { get; set; }
        [Required] public bool Enabled { get; set; }
    }
}
