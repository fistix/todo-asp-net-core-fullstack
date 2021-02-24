using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Customers
{
  public class SampleCheckoutCommand : IRequest<SampleCheckoutCommandResult>
  {
    public string Email { get; set; }
    public long Amount { get; set; }
    public string ProductName { get; set; }
  }
}
