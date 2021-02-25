using Fistix.Training.Domain.Commands.Stripe;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.Stripe
{
  public class SampleCheckoutCommandHandler : IRequestHandler<SampleCheckoutCommand, SampleCheckoutCommandResult>
  {
    private readonly StripeService _stripeService = null;
    public SampleCheckoutCommandHandler(StripeService stripeService)
    {
      _stripeService = stripeService;
    }
    public async Task<SampleCheckoutCommandResult> Handle(SampleCheckoutCommand command, CancellationToken cancellationToken)
    {

      var sessionId = await _stripeService.SampleCheckout(command);

      return new SampleCheckoutCommandResult()
      {
        SessionId = sessionId
      };
      
    }
  }
}
