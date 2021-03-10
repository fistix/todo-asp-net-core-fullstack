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
  public class GetSubscriptionPlanDetailByIdQueryHandler : IRequestHandler<GetSubscriptionPlanDetailByIdQuery, GetSubscriptionPlanDetailByIdQueryResult>
  {
    private readonly PayPalService _payPalService = null;
    public GetSubscriptionPlanDetailByIdQueryHandler(PayPalService payPalService)
    {
      _payPalService = payPalService;
    }
    public async Task<GetSubscriptionPlanDetailByIdQueryResult> Handle(GetSubscriptionPlanDetailByIdQuery query, CancellationToken cancellationToken)
    {
      var result = await _payPalService.GetSubscriptionPlanDetailById(query);

      return new GetSubscriptionPlanDetailByIdQueryResult()
      {
        Payload = result
      };
      //throw new NotImplementedException();
    }
  }
}
