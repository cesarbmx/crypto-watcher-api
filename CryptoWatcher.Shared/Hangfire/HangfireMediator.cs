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

        public void Send(IRequest request)
        {
            _mediator.Send(request);
        }
    }
}
