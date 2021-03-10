using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class SubscriptionPlanDetailsByPlanIdModel
  {
    public string Id { get; set; }

    [JsonProperty("product_id")]
    public string ProductId { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }

    [JsonProperty("usage_type")]
    public string UsageType { get; set; }

    public List<BillingCycle> billing_cycles { get; set; }
    public PaymentPreferences payment_preferences { get; set; }

    [JsonProperty("quantity_supported")]
    public string QuantitySupported { get; set; }

    [JsonProperty("create_time")]
    public string CreateTime { get; set; }

    [JsonProperty("update_time")]
    public string UpdateTime { get; set; }


  }
}
