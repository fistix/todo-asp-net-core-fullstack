using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Fistix.Training.Domain.PayPalModels
{
  public class CreatePlanModel
  {
    //With JsonProperty, model is filled but Plan is not creating on PayPal, response is false - ResponseMessage: Request is not well-formed
    
    //[JsonProperty("product_id")]
    [JsonPropertyName("product_id")]
    public string ProductId { get; set; }


    //[JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }


    //[JsonProperty("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }


    //[JsonProperty("billing_cycles")]
    [JsonPropertyName("billing_cycles")]
    public List<BillingCycle> BillingCycles { get; set; }


    //[JsonProperty("payment_preferences")]
    [JsonPropertyName("payment_preferences")]
    public PaymentPreferences PaymentPreferences { get; set; }


    //[JsonProperty("taxes")]
    [JsonPropertyName("taxes")]
    public Taxes Taxes { get; set; }

  }
}
