using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Customers
{
  public class CreateSessionCommand : IRequest<CreateSessionCommandResult>
  {
  }
}
