using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Paypal
{
  public class CreateOrderCommand : IRequest<CreateOrderCommandResult>
  {
  }
}
