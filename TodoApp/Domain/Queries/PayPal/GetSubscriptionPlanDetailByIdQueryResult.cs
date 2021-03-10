using Fistix.Training.Domain.PayPalModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Queries.PayPal
{
  public class GetSubscriptionPlanDetailByIdQueryResult
  {
    public SubscriptionPlanDetailsByPlanIdModel Payload { get; set; }
  }
}
