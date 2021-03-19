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
  public class GetAllProductDetailsQueryHandler : IRequestHandler<GetAllProductDetailsQuery, GetAllProductDetailsQueryResult>
  {
    private readonly PayPalService _payPalService = null;
    public GetAllProductDetailsQueryHandler(PayPalService payPalService)
    {
      _payPalService = payPalService;
    }
    public async Task<GetAllProductDetailsQueryResult> Handle(GetAllProductDetailsQuery request, CancellationToken cancellationToken)
    {
      var products = await _payPalService.GetAllProductDetails();
      
      return new GetAllProductDetailsQueryResult()
      {
        Payload = products
      };
      throw new NotImplementedException();
    }
  }
}
