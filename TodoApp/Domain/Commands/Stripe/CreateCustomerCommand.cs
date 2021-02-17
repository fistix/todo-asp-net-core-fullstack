using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Stripe
{
  public class CreateCustomerCommand : IRequest<CreateCustomerCommandResult>
  {
    public string Email { get; set; }
  }
}
