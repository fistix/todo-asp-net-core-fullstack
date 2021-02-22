using Fistix.Training.Domain.Commands.Stripe;
using MediatR;
using Stripe.Checkout;
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
    public async Task<SampleCheckoutCommandResult> Handle(SampleCheckoutCommand command, CancellationToken cancellationToken)
    {
      var domain = "https://localhost:5200";

      var options = new SessionCreateOptions
      {

        CustomerEmail = command.Email,
        PaymentMethodTypes = new List<string>
                {
                  "card",
                },
        LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = command.Amount,
                      Currency = "usd",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = command.ProductName,
                      },
                    },
                    Quantity = 1,
                  },
                },
        Mode = "payment",
        SuccessUrl = domain + "/checkoutSampleSuccess",
        CancelUrl = domain + "/checkoutSampleCancel",
      };
      var service = new SessionService();
      Session session = service.Create(options);

      return new SampleCheckoutCommandResult()
      {
        SessionId = session.Id
      };
      
    }
  }
}
