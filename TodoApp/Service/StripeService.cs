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

    public async Task<Customer> GetByEmail(string email)
    {
      try
      {
        #region GetCustomersList
        var CustomerOptions = new CustomerListOptions
        {
          Limit = 50,
          Email = email,
          //RnD about extra parameters
          //Created = DateTime.Now,
          //StartingAfter = DateTime.Now.ToString(),
          //EndingBefore = DateTime.Now.ToString(),
        };

        var customerService = new CustomerService();
        StripeList<Customer> stripeCustomersList = customerService.List(CustomerOptions);
        #endregion

        Customer stripeCustomer = new Customer();

        if (stripeCustomersList.Any(x => x.Email.Equals(email)))
          stripeCustomer = stripeCustomersList.FirstOrDefault(x => x.Email.Equals(email));

        return stripeCustomer;
      }

      catch (StripeException e)
      {
        string errorMessage = "";
        switch (e.StripeError.Error)
        {
          case "card_error":
            errorMessage = $"Card Error occurred on {e.StripeError.PaymentIntent.Id}, Error: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "api_error":
            errorMessage = $"API Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "api_connection_error":
            errorMessage = $"API Connection Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "invalid_request_error	":
            errorMessage = $"Invalid request Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          default:
            errorMessage = $"Some Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
        }

        throw new InvalidOperationException(errorMessage);
      }

    }

    public async Task<Customer> Create(CreateCustomerCommand command)
    {
      try
      {
        var customerService = new CustomerService();
        Customer stripeCustomer = customerService.Create(new CustomerCreateOptions
        {
          Email = command.Email,
          Name = command.FirstName + " " + command.LastName,
          //RnD about extra parameters
          //Description = "Additional description about customer",
          //Phone = "897877745464",
        });

        return stripeCustomer;
      }

      catch (StripeException e)
      {
        string errorMessage = "";
        switch (e.StripeError.Error)
        {
          case "card_error":
            errorMessage = $"Card Error occurred on {e.StripeError.PaymentIntent.Id}, Error: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "api_error":
            errorMessage = $"API Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "api_connection_error":
            errorMessage = $"API Connection Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "invalid_request_error	":
            errorMessage = $"Invalid request Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          default:
            errorMessage = $"Some Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
        }

        throw new InvalidOperationException(errorMessage);
      }

    }

    public async Task<string> SampleCheckout(SampleCheckoutCommand command)
    {
      try
      {
        //var domain = "https://localhost:5200";
        var domain = _masterConfig.StripeConfig.Domain;

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
          SuccessUrl = _masterConfig.StripeConfig.SuccessUrl,
          CancelUrl = _masterConfig.StripeConfig.CancelUrl,
          //SuccessUrl = domain + "/checkoutSampleSuccess",
          //CancelUrl = domain + "/checkoutSampleCancel",
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return session.Id;
      }

      catch (StripeException e)
      {
        string errorMessage = "";
        switch (e.StripeError.Error)
        {
          case "card_error":
            errorMessage = $"Card Error occurred on {e.StripeError.PaymentIntent.Id}, Error: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "api_error":
            errorMessage = $"API Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "api_connection_error":
            errorMessage = $"API Connection Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "invalid_request_error	":
            errorMessage = $"Invalid request Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          default:
            errorMessage = $"Some Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
        }

        throw new InvalidOperationException(errorMessage);
      }
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
        string errorMessage = "";
        switch (e.StripeError.Error)
        {
          case "card_error":
            errorMessage = $"Card Error occurred on {e.StripeError.PaymentIntent.Id}, Error: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "api_error":
            errorMessage = $"API Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "api_connection_error":
            errorMessage = $"API Connection Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          case "invalid_request_error	":
            errorMessage = $"Invalid request Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
          default:
            errorMessage = $"Some Error occurred: {e.StripeError.Error}, Error Code: {e.StripeError.Code}, Error Description: {e.StripeError.ErrorDescription}";
            break;
        }

        throw new InvalidOperationException(errorMessage);
      }

    }
  }
}

