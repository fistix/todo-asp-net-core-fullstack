using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  //Additional for OrderModel instead of Order default model of PayPal

  public class AddressPortable
  {
    public AddressDetails AddressDetails { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string AdminArea1 { get; set; }
    public string AdminArea2 { get; set; }
    public string AdminArea3 { get; set; }
    public string AdminArea4 { get; set; }
    public string CountryCode { get; set; }
    public string PostalCode { get; set; }
  }
}
