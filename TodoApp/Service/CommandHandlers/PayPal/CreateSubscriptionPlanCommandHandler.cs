using Fistix.Training.Domain.Commands.Paypal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.PayPal
{
  public class CreateSubscriptionPlanCommandHandler : IRequestHandler<CreateSubscriptionPlanCommand, CreateSubscriptionPlanCommandResult>
  {
    private readonly PayPalService _payPalService = null;
    public CreateSubscriptionPlanCommandHandler(PayPalService payPalService)
    {
      _payPalService = payPalService;
    }
    public async Task<CreateSubscriptionPlanCommandResult> Handle(CreateSubscriptionPlanCommand command, CancellationToken cancellationToken)
    {
      var plan = await _payPalService.CreatePlan();

      return new CreateSubscriptionPlanCommandResult()
      {
        Payload = plan
      };

    }
  }
}
