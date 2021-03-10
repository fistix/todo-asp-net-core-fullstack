using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class SubscriptionPlanDetailModel
  {

    public List<Plan> Plans { get; set; }
    //public string Id { get; set; }

    //[JsonProperty("product_id")]
    //public string ProductId { get; set; }
    //public string Name { get; set; }
    //public string Status { get; set; }
    //public string Description { get; set; }

    //[JsonProperty("usage_type")]
    //public string UsageType { get; set; }
  }
}
