using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Dtos
{
  public class CustomerDto
  {
    public Guid? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string StripeCustomerId { get; set; }
    public string StripeCustomerName { get; set; }
    public long? LastPaymentDeduct { get; set; }
    public DateTime? LastPaymentDeductOn { get; set; }

  }
}
