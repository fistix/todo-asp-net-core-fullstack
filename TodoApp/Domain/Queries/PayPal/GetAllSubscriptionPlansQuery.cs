using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Queries.PayPal
{
  public class GetAllSubscriptionPlansQuery : IRequest<GetAllSubscriptionPlansQueryResult>
  {
  }
}
