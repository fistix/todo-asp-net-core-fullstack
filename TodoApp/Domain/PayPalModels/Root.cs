using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class Root
  {
    public string id { get; set; }
    public string create_time { get; set; }
    public string event_type { get; set; }
    public string event_version { get; set; }
    public string resource_type { get; set; }
    public string resource_version { get; set; }
    public string summary { get; set; }
    public Resource resource { get; set; }
    public List<Link> links { get; set; }


    //Additional properties for GetAllTransactionsDetailsBySubscriptionIdModel
    public string product_id { get; set; }
    public string name { get; set; }
    public string status { get; set; }
    public string description { get; set; }
    public string usage_type { get; set; }
    public List<BillingCycle> billing_cycles { get; set; }
    public PaymentPreferences payment_preferences { get; set; }
    public bool quantity_supported { get; set; }
    public DateTime update_time { get; set; }


    //Additional properties for GetAllTransactionsDetailsBySubscriptionIdModel
    public List<Transaction> transactions { get; set; }
  }
}
