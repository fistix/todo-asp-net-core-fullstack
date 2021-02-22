using Fistix.Training.Domain.Commands.Stripe;
using MediatR;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.Stripe
{
  public class PaymentDeductionCommandHandler : IRequestHandler<PaymentDeductionCommand, PaymentDeductionCommandResult>
  {
    public async Task<PaymentDeductionCommandResult> Handle(PaymentDeductionCommand command, CancellationToken cancellationToken)
    {
      //var amountt=long.Parse(amount);
      var methodOptions = new PaymentMethodListOptions
      {
        Customer = command.CustomerId,
        Type = "card",
      };

      var methodService = new PaymentMethodService();
      var paymentMethods = methodService.List(methodOptions);

      //To get the first payment method
      var payment = paymentMethods.ToList().FirstOrDefault();

      var service = new PaymentIntentService();
      var options = new PaymentIntentCreateOptions
      {
        Amount = command.Amount,
        Currency = "usd",
        Customer = command.CustomerId,
        PaymentMethod = payment.Id,
        Confirm = true,
        OffSession = true,
      };
      var paymentIntent = service.Create(options);

      return new PaymentDeductionCommandResult()
      {
        PaymentIntentId = paymentIntent.Id
      };

      //throw new NotImplementedException();
    }
  }
}
