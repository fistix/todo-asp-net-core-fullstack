using Fistix.Training.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Customers
{
  public class CreateCustomerCommandResult
  {
    //public string Id { get; set; }
    //public string Email { get; set; }
    //public string FullName { get; set; }
    public CustomerDto Payload { get; set; }
  }
}
