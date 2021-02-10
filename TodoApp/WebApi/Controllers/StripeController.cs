using Domain.Commands.Strip;
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
  //[Route("webhook")]

  [ApiController]
  public class StripeController : ControllerBase
  {

    // You can find your endpoint's secret in your webhook settings
    const string secret = "whsec_82Ut3MRXob9ZDzQwquSq1Eal7uXVKI4C";

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
          this.FulfillOrder(session);
        }

        return Ok();
      }
      catch (StripeException e)
      {
        return BadRequest();
      }
      catch(Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    private void FulfillOrder(Session session)
    {
      // TODO: fill me in
      //throw new NotImplementedException();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create()
    {
      //await Index();

      var domain = "https://localhost:5200";
      var options = new SessionCreateOptions
      {
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
                      UnitAmount = 2000,
                      Currency = "usd",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = "Stubborn Attachments",
                      },
                    },
                    Quantity = 1,
                  },
                },
        Mode = "payment",
        SuccessUrl = domain + "/checkoutSuccess",
        CancelUrl = domain + "/checkoutCancel",
      };
      var service = new SessionService();
      Session session = service.Create(options);



      return Ok(new CreateSessionCommandResult(){ SessionId = session.Id });
    }



  }
}
