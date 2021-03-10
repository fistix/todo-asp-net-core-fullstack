using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Paypal
{
  public class CaptureOrderCommand : IRequest<CaptureOrderCommandResult>
  {
    public string OrderId { get; set; }
  }
}
