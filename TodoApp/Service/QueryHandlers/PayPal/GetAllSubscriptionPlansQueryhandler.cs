using Fistix.Training.Domain.Queries.PayPal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.QueryHandlers.PayPal
{
  public class GetAllSubscriptionPlansQueryhandler : IRequestHandler<GetAllSubscriptionPlansQuery, GetAllSubscriptionPlansQueryResult>
  {
    private readonly PayPalService _payPalService = null;
    public GetAllSubscriptionPlansQueryhandler(PayPalService payPalService)
    {
      _payPalService = payPalService;
    }

    public async Task<GetAllSubscriptionPlansQueryResult> Handle(GetAllSubscriptionPlansQuery request, CancellationToken cancellationToken)
    {
      var plans = await _payPalService.GetAllSubscriptionPlans();

      return new GetAllSubscriptionPlansQueryResult()
      {
        Payload = plans
      };
      //throw new NotImplementedException();
    }
  }
}
