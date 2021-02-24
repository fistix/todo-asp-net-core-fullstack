using Fistix.Training.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Queries.Stripe
{
  public class GetCustomerDetailByEmailQueryResult
  {
    public string CustomerId { get; set; }
    public CustomerDto Payload { get; set; }
  }
}
