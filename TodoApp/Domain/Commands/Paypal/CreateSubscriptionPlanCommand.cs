using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Fistix.Training.Domain.Commands.Paypal
{
  public class CreateSubscriptionPlanCommand : IRequest<CreateSubscriptionPlanCommandResult>
  {
    //[JsonPropertyName("dsadsa")]
    public string ProductId { get; set; }
    public string PlanName { get; set; }
    public string PlanDescription { get; set; }
    public string SubscriptionSetupFee { get; set; }
    //public string CurrencyCode { get; set; }
    public int BillingCycles { get; set; }
    //public string Taxpercentage { get; set; }
  }
}
