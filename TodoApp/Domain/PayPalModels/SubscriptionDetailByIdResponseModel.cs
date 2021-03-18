using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class SubscriptionDetailByIdResponseModel
  {
    public string Id { get; set; }
    public string Status { get; set; }

    [JsonProperty("plan_id")]
    public string PlanId { get; set; }

    [JsonProperty("subscriber")]
    public Subscriber subscriber { get; set; }

    public BillingInfo billing_info { get; set; }

  }
}
