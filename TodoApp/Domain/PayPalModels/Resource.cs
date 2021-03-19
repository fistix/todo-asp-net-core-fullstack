using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class Resource
  {
    public string id { get; set; }
    public string status { get; set; }
    public DateTime status_update_time { get; set; }
    public string plan_id { get; set; }
    public DateTime start_time { get; set; }
    public string quantity { get; set; }
    public ShippingAmount shipping_amount { get; set; }
    public Subscriber subscriber { get; set; }
    public bool auto_renewal { get; set; }
    public BillingInfo billing_info { get; set; }
    public DateTime create_time { get; set; }
    public DateTime update_time { get; set; }
    public List<Link> links { get; set; }

    //public Payer Payer { get; set; }
  }
}
