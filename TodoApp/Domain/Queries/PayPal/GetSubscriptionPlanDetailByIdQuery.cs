using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Queries.PayPal
{
  public class GetSubscriptionPlanDetailByIdQuery : IRequest<GetSubscriptionPlanDetailByIdQueryResult>
  {
    public string Id { get; set; }
  }
}
