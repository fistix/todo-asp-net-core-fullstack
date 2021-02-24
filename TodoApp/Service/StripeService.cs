using Fistix.Training.Domain.Commands.Customers;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Service
{
  public class StripeService
  {
    public async Task<Customer> Create(CreateCustomerCommand command)
    {
      //Customer stripeCustomer = new Customer();

      //var stripeCustomer = await GetByEmail(command.Email);

      //if (stripeCustomer == null)
      //{
        var customerService = new CustomerService();
        Customer stripeCustomer = customerService.Create(new CustomerCreateOptions
        {
          Email = command.Email,
          Name = command.FirstName + " " + command.LastName
        });
      //}

      return stripeCustomer;
    }

    public async Task<Customer> GetByEmail(string email)
    {
      #region GetCustomersList
      var CustomerOptions = new CustomerListOptions
      {
        Limit = 50,
        Email = email
        //RnD about extra parameters
      };
      var customerService = new CustomerService();
      StripeList<Customer> stripeCustomersList = customerService.List(
        CustomerOptions
        );
      #endregion

      Customer stripeCustomer = new Customer();

      if (stripeCustomersList.Any(x => x.Email.Equals(email)))
      {
        stripeCustomer = stripeCustomersList.FirstOrDefault(x => x.Email.Equals(email));
        //stripeCustomer.Id = temp.Id;
      }
      return stripeCustomer;

    }


    public async Task<PaymentIntent> PaymentDeduct(PaymentDeductionCommand command)
    {
      //var amountt=long.Parse(amount);
      var methodOptions = new PaymentMethodListOptions
      {
        Customer = command.StripeCustomerId,
        Type = "card",
      };

      var methodService = new PaymentMethodService();
      var paymentMethods = methodService.List(methodOptions);

      //To get the first payment method
      var payment = paymentMethods.ToList().FirstOrDefault();

      var service = new PaymentIntentService();
      var options = new PaymentIntentCreateOptions
      {
        Amount = command.Amount,
        Currency = "usd",
        Customer = command.StripeCustomerId,
        PaymentMethod = payment.Id,
        Confirm = true,
        OffSession = true,
      };
      var paymentIntent = service.Create(options);
      return paymentIntent;
    }
  }
}
