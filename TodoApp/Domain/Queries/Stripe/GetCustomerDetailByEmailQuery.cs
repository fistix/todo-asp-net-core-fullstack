using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Queries.Stripe
{
  public class GetCustomerDetailByEmailQuery : IRequest<GetCustomerDetailByEmailQueryResult>
  {
    public string Email { get; set; }
  }
}
