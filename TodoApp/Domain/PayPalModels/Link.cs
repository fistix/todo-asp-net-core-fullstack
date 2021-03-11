using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class Link
  {
    public string href { get; set; }
    public string rel { get; set; }
    public string method { get; set; }

    //Additional property for GetSubscriptionPlansListModel
    public string encType { get; set; }

    //Additional for OrderModel instead of Order default model of PayPal
    public string EncType { get; set; }
    public string MediaType { get; set; }
    public string Title { get; set; }
  }
}
