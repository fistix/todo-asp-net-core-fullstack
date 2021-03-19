using Fistix.Training.Domain.PayPalModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Paypal
{
  public class CreateOrderCommand : IRequest<CreateOrderCommandResult>
  {
    public string ShippingName { get; set; }
    public string ShippingAddressLine1 { get; set; }
    public string ShippingAddressLine2 { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingState { get; set; }
    public string ShippingPostalCode { get; set; }
    public string ShippingCountryCode { get; set; }


    public ShippingDetail ShippingDetail { get; set; }
  }
}
