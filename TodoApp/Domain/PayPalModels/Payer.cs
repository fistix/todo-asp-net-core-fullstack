using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  //Additional for OrderModel instead of Order default model of PayPal

  public class Payer
  {
    public AddressPortable AddressPortable { get; set; }
    public string BirthDate { get; set; }
    public string Email { get; set; }
    public Name Name { get; set; }
    public string PayerId { get; set; }
    //public PhoneWithType PhoneWithType { get; set; }
    //public TaxInfo TaxInfo { get; set; }
  }
}
