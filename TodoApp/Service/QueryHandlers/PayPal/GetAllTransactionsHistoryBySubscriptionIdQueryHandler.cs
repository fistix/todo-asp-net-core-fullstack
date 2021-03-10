using Fistix.Training.Domain.Queries.PayPal;
using Fistix.Training.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.QueryHandlers.PayPal
{
  public class GetAllTransactionsHistoryBySubscriptionIdQueryHandler : IRequestHandler<GetAllTransactionsHistoryBySubscriptionIdQuery, GetAllTransactionsHistoryBySubscriptionIdQueryResult>
  {
    private readonly PayPalService _payPalService = null;
    public GetAllTransactionsHistoryBySubscriptionIdQueryHandler(PayPalService payPalService)
    {
      _payPalService = payPalService;
    }
    public async Task<GetAllTransactionsHistoryBySubscriptionIdQueryResult> Handle(GetAllTransactionsHistoryBySubscriptionIdQuery query, CancellationToken cancellationToken)
    {
      var result = await _payPalService.GetAllTransactionsDetailsBySubscriptionId(query);

      return new GetAllTransactionsHistoryBySubscriptionIdQueryResult()
      {
        Payload = result
      };
      throw new NotImplementedException();
    }
  }
}
