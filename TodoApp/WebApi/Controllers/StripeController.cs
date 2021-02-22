using Fistix.Training.Core.Exceptions;
using Fistix.Training.Domain.Commands.Stripe;
using Fistix.Training.Domain.Queries.Stripe;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  [Authorize]

  public class StripeController : ControllerBase
  {
    private readonly IMediator _mediator = null;

    public StripeController(IMediator mediator)
    {
      _mediator = mediator;
    }

    // You can find your endpoint's secret in your webhook settings
    const string secret = "whsec_82Ut3MRXob9ZDzQwquSq1Eal7uXVKI4C";

    private static string PublicKey = "pk_test_51IIUm0KsuYyFXhSvPIN8vpVEOwJuLMLVqoqBEwPVOXO3RC2Rh8CTRs2kxWbK51SQWsR8mBvAIltGDMY0bjheLavT00NKFlsOxO";
    //private static string CustomerId { get; set; }/* = "cus_IxHNEF1I7JCgZl";*/
    //private static string PaymentId { get; set; } = "pm_1IMBZtKsuYyFXhSv758bkjyd";
    //private static string PaymentMethodId = "";

    private static int ProductAmount { get; set; } = 2000;


    //[Route("webhook")]
    [HttpPost("index")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Index()
    {
      try
      {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var stripeEvent = EventUtility.ConstructEvent(
          json,
          Request.Headers["Stripe-Signature"],
          secret
        );

        // Handle the checkout.session.completed event
        if (stripeEvent.Type == Events.CheckoutSessionCompleted)
        {
          var session = stripeEvent.Data.Object as Session;

          return Content("Order is Fulfilled!");

          // Fulfill the purchase...
          //this.FulfillOrder(session);
        }

        return Ok();
      }
      catch (StripeException e)
      {
        return BadRequest();
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    private void FulfillOrder(Session session)
    {
      // TODO: fill me in
      //throw new NotImplementedException();
    }

    #region Server

    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreateCustomerCommandResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return base.BadRequest(ModelState);
        }

        var result = await _mediator.Send<CreateCustomerCommandResult>(command);
        return base.Created($"api/Stripe/Create/{result.Id}", result);
        //return base.Created($"api/Stripe/{result.Payload.Id}", result);
        //return Ok(new CreateCustomerCommandResult() { CustomerId = customer.Id });
      }

      catch (InvalidOperationException ex)
      {
        return base.Conflict(ex.Message);
      }

    }


    [HttpGet("ByEmail")]
    [ProducesResponseType(typeof(GetCustomerDetailByEmailQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
      try
      {
        if (String.IsNullOrWhiteSpace(email))
        {
          return base.BadRequest();
        }

        var query = new GetCustomerDetailByEmailQuery() { Email = email };

        var result = await _mediator.Send(query);

        return base.Ok(result);
      }
      catch (NotFoundException)
      {
        return base.NotFound();
      }
    }


    [HttpPost("PaymentDeduction")]
    [ProducesResponseType(typeof(PaymentDeductionCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PaymentDeduction(PaymentDeductionCommand command)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return base.BadRequest(ModelState);
        }

        var result = await _mediator.Send<PaymentDeductionCommandResult>(command);
        return base.Ok(result);
        //return base.Ok($"api/Stripe/Create/{result.PaymentIntentId}", result);
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
        return null;
      }
    }

    [HttpPost("SampleCheckout")]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(SampleCheckoutCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SampleCheckout([FromBody] SampleCheckoutCommand command)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return base.BadRequest(ModelState);
        }

        var result = await _mediator.Send<SampleCheckoutCommandResult>(command);
        return base.Ok(result);
      }
      catch (Exception)
      {

        throw;
      }
      return null;
      //return Ok(new CreateSessionCommandResult() { SessionId = session.Id });
    }


    #endregion


    [HttpPost("CheckoutSample")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckoutSample(string Email, long amount, string productName)
    {
      //await Index();

      var domain = "https://localhost:5200";

      //var sessionListOptions = new SessionListOptions
      //{
      //  Limit = 3,
      //};
      //var ssessionService = new SessionService();
      //StripeList<Session> sessions = ssessionService.List(
      //  sessionListOptions
      //);

      var options = new SessionCreateOptions
      {
         
        CustomerEmail = Email,
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
                      UnitAmount = amount,
                      Currency = "usd",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = productName/*"Stubborn Attachments"*/,
                      },
                    },
                    Quantity = 1,
                  },
                },
        Mode = "payment",
        SuccessUrl = domain + "/checkoutSampleSuccess",
        CancelUrl = domain + "/checkoutSampleCancel",
      };
      var service = new SessionService();
      Session session = service.Create(options);

      return Ok(new CreateSessionCommandResult() { SessionId = session.Id });
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
    {
      Customer customer = new Customer();

      #region GetCustomersList
      var CustomerOptions = new CustomerListOptions
      {
        Limit = 50,
        Email = command.Email
      };
      var customerService = new CustomerService();
      StripeList<Customer> customersList = customerService.List(
        CustomerOptions
        );
      #endregion

      if (customersList.Any(x => x.Email.Equals(command.Email)))
      {
        var temp = customersList.FirstOrDefault(x => x.Email.Equals(command.Email));
        customer.Id = temp.Id;
      }
      else
      {
        customer = customerService.Create(new CustomerCreateOptions
        {
          Email = command.Email
        });
      }

      //CustomerId = customer.Id;
      return Ok(new CreateCustomerCommandResult() { Id = customer.Id });

    }


    [HttpPost("CreatePaymentIntent")]
    //[HttpPost("create-payment-intent")]
    public async Task<IActionResult> Checkout()
    {
      var optionss = new PaymentIntentCreateOptions
      {
        Amount = ProductAmount/*1099*/,
        Currency = "usd",
        SetupFutureUsage = "off_session",
        //Customer = CustomerId
      };

      var servicee = new PaymentIntentService();
      var paymentIntent = servicee.Create(optionss);

      //return null;
      return Ok(new
      {
        PublicKey,
        ClientSecret = paymentIntent.ClientSecret,
        Id = paymentIntent.Id
      });

    }


    [HttpPost("OffSessionPayment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult OffSessionPayment(string customerId, long amount)
    {
      try
      {
        //var amountt=long.Parse(amount);
        var methodOptions = new PaymentMethodListOptions
        {
          Customer = customerId,
          Type = "card",
        };

        var methodService = new PaymentMethodService();
        var paymentMethods = methodService.List(methodOptions);

        //To get the first payment method
        var payment = paymentMethods.ToList().FirstOrDefault();

        var service = new PaymentIntentService();
        var options = new PaymentIntentCreateOptions
        {
          Amount = amount/*ProductAmount*//*1099*/,
          Currency = "usd",
          Customer = customerId,
          PaymentMethod = /*PaymentId*//*"card"*/payment.Id,
          Confirm = true,
          OffSession = true,
        };
        var paymentIntentt = service.Create(options);

        return Ok();
        //return View("ButtonsView");

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
        return null;
      }
    }





    [HttpGet("CreateAndSave")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAndSave()
    {
      //Step 2
      var options = new CustomerCreateOptions { };

      var service = new CustomerService();
      var customer = service.Create(options);

      //Step 3
      var optionss = new PaymentIntentCreateOptions
      {
        Amount = 1099,
        Currency = "usd",
        Customer = customer.Id //"{{CUSTOMER_ID}}",
      };

      var servicee = new PaymentIntentService();
      var paymentIntent = servicee.Create(optionss);
      return Ok(new SaveCustomerDetailsCommandResult() { ClientSecret = paymentIntent.ClientSecret });

    }



  }
}
