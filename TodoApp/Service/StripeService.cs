using Fistix.Training.Core.Config;
using Fistix.Training.Domain.Commands.Customers;
using Fistix.Training.Domain.Commands.Stripe;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Service
{
  public class StripeService
  {
    private readonly MasterConfig _masterConfig = null;
    public StripeService(MasterConfig masterConfig)
    {
      _masterConfig = masterConfig;
    }

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

    public async Task<bool> PaymentDeduct(PaymentDeductCommand command)
    {
      try
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
        return true;
      }

      catch (StripeException e)
      {
        switch (e.StripeError.Error/*.ErrorType*/)
        {
          case "card_error":
            // Error code will be authentication_required if authentication is needed
            Console.WriteLine("Error code: " + e.StripeError.Code);
            var paymentIntentId = e.StripeError.PaymentIntent.Id;
            var service = new PaymentIntentService();
            var paymentIntent = service.Get(paymentIntentId);

            Console.WriteLine(paymentIntent.Id);
            break;
          default:
            break;
        }
        ////
        return false;

      }
    }
    public async Task<string> SampleCheckout(SampleCheckoutCommand command)
    {
      var domain = "https://localhost:5200";
      //var domain = _masterConfig.StripeConfig.Domain;

      var options = new SessionCreateOptions
      {
        CustomerEmail = command.Email,
        PaymentMethodTypes = new List<string>
                {
                  "card",
                },
        LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = command.Amount,
                      Currency = "usd",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = command.ProductName,
                      },
                    },
                    Quantity = 1,
                  },
                },
        Mode = "payment",
        SuccessUrl = domain + "/checkoutSampleSuccess",
        //SuccessUrl = _masterConfig.StripeConfig.SuccessUrl,
        CancelUrl = domain + "/checkoutSampleCancel",
        //CancelUrl = _masterConfig.StripeConfig.CancelUrl,
      };
      var service = new SessionService();
      Session session = service.Create(options);

      return session.Id;
    }
  }
}
