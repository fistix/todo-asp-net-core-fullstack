using Fistix.Training.Domain.PayPalModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Queries.PayPal
{
  public class GetAllSubscriptionPlansQueryResult
  {
    public List<Plan> Plans { get; set; }
    //public SubscriptionPlanDetailModel Payload { get; set; }
  }
}
