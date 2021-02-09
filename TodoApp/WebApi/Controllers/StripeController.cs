using Domain.Commands.Strip;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StripeController : ControllerBase
  {


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create()
    {
      var domain = "https://localhost:5001";
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
        SuccessUrl = domain + "/success.html",
        CancelUrl = domain + "/cancel.html",
      };
      var service = new SessionService();
      Session session = service.Create(options);
      return Ok(new CreateSessionCommandResult(){ SessionId = session.Id });
    }
  }
}
