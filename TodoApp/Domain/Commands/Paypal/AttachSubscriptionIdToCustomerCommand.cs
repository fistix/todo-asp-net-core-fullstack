using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Paypal
{
  public class AttachSubscriptionIdToCustomerCommand : IRequest<AttachSubscriptionIdToCustomerCommandResult>
  {
    public string Id { get; set; }
  }
}
