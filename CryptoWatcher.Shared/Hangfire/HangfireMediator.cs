using System.Threading.Tasks;
using MediatR;

namespace CryptoWatcher.Shared.Hangfire
{

    public class HangfireMediator
    {
        private readonly IMediator _mediator;

        public HangfireMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Send( IRequest request )
        {
            await _mediator.Send(request);
        }
        public async Task Send(params IRequest[] requests)
        {
            foreach (IRequest request in requests)
                await _mediator.Send(request);
        }
    }
}
