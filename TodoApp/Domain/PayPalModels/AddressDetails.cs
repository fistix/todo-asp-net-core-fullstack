using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  //Additional for OrderModel instead of Order default model of PayPal

  public class AddressDetails
  {
    public string BuildingName { get; set; }
    public string DeliveryService { get; set; }
    public string StreetName { get; set; }
    public string StreetNumber { get; set; }
    public string StreetType { get; set; }
    public string SubBuilding { get; set; }
  }
}
