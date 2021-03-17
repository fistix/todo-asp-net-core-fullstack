using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class CreatePlanModel
  {
    //[JsonProperty("product_id")]
    public string product_id { get; set; }

    //[JsonProperty("name")]
    public string name { get; set; }

    //[JsonProperty("description")]
    public string description { get; set; }

    //[JsonProperty("billing_cycles")]
    public List<BillingCycle> billing_cycles { get; set; }

    //[JsonProperty("payment_preferences")]
    public PaymentPreferences payment_preferences { get; set; }

    //[JsonProperty("taxes")]
    public Taxes taxes { get; set; }



    //[JsonProperty("product_id")]
    //public string ProductId { get; set; }

    //[JsonProperty("name")]
    //public string Name { get; set; }

    //[JsonProperty("description")]
    //public string Description { get; set; }

    //[JsonProperty("billing_cycles")]
    //public List<BillingCycle> BillingCycles { get; set; }

    //[JsonProperty("payment_preferences")]
    //public PaymentPreferences PaymentPreferences { get; set; }

    //[JsonProperty("taxes")]
    //public Taxes Taxes { get; set; }

  }
}
