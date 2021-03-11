using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  //Additional for OrderModel instead of Order default model of PayPal

  public class OrderModel
  {
    public string CheckoutPaymentIntent { get; set; }
    public string CreateTime { get; set; }
    public string ExpirationTime { get; set; }
    public string Id { get; set; }
    public List<LinkDescription> Links { get; set; }
    public PayPalCheckoutSdk.Orders.Payer Payer { get; set; }
    public List<PurchaseUnit> PurchaseUnits { get; set; }
    public string Status { get; set; }
    public string UpdateTime { get; set; }
  }
}
