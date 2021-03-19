using Fistix.Training.Domain.Commands.Paypal;
using Fistix.Training.Domain.PayPalModels;
using Fistix.Training.Domain.Queries.PayPal;
using Fistix.Training.WebApi.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  //[Authorize]
  public class PayPalController : ControllerBase
  {
    private readonly IMediator _mediator = null;
    //private readonly HttpClient _httpClient = null;
    //private readonly IHttpContextAccessor _contextAccessor = null;
    public const string BearerToken = "A21AAKlEi6igztWCovpWY33ClG99N6S6KI4d7qRJT-zY6ZZJMPnHgUUo-Op6aFXJyWI3mHtl0HfwF6Mu-sA_orskYcx6P3OoQ";

    public PayPalController(IMediator mediator/*, HttpClient httpClient, IHttpContextAccessor contextAccessor*/)
    {
      _mediator = mediator;
      //_httpClient = httpClient;
      //_contextAccessor = contextAccessor;
    }

    #region OneTimeCheckout

    [HttpPost("CreateOrder")]
    [ProducesResponseType(typeof(CreateOrderCommandResult), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
      try
      {
        //if (!ModelState.IsValid)
        //  return base.BadRequest(ModelState);

        var result = await _mediator.Send<CreateOrderCommandResult>(new CreateOrderCommand());
        ////
        return Ok(result);
        //return Created($"api/PayPal/{result}",result);
      }

      ///
      catch (Exception)
      {

        throw;
      }

    }

    [HttpPost("CaptureOrder/{orderId}")]
    [ProducesResponseType(typeof(CaptureOrderCommandResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CaptureOrder([FromRoute] string orderId /*CaptureOrderCommand command*/)
    {
      try
      {
        //if (!ModelState.IsValid)
        //  return base.BadRequest(ModelState);

        if (String.IsNullOrEmpty(orderId))
          return base.BadRequest(ModelState);

        var command = new CaptureOrderCommand() { OrderId = orderId };

        var result = await _mediator.Send<CaptureOrderCommandResult>(command);
        ////
        return Ok(result);
        //return Created($"api/PayPal/{result}",result);
      }

      ///
      catch (Exception)
      {

        throw;
      }

    }

    #endregion

    #region API calls

    //[HttpGet("GetAllTransactions/{subscriptionId}")]
    [HttpGet("GetAllTransactionsBySubscriptionId")]
    [ProducesResponseType(typeof(GetAllTransactionsHistoryBySubscriptionIdQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllTransactionsHistoryBySubscriptionId(/*[FromRoute] string SubscriptionId, */[FromQuery] GetAllTransactionsHistoryBySubscriptionIdQuery query)
    {
      //JsonIgnore not working in FromQuery tag
      //Should I use FromQuery tag to get all fields? And remove subscriptionId from endpoint?
      try
      {
        //SubscriptionId = "I-SX5SXUFJH4KW";
        //query.SubscriptionId = SubscriptionId;
        query.SubscriptionId = "I-SX5SXUFJH4KW";

        if (!ModelState.IsValid)
          return base.BadRequest(ModelState);

        //var query = new GetAllTransactionsHistoryBySubscriptionIdQuery() 
        //{
        //  Id = Id
        //};

        var result = await _mediator.Send<GetAllTransactionsHistoryBySubscriptionIdQueryResult>(query);
        return Ok(result);

      }

      ///
      catch (Exception)
      {

        throw;
      }

    }


    [HttpGet("GetSubscriptionPlanDetailById/{planId}")]
    [ProducesResponseType(typeof(GetSubscriptionPlanDetailByIdQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSubscriptionPlanDetailById([FromRoute] string planId/*, [FromBody] GetSubscriptionPlanDetailByIdQuery query*/)
    {
      try
      {
        planId = "P-1GP34431BG4466351MBAOLLA";
        //query.Id = Id;

        //if (!ModelState.IsValid)
        if (String.IsNullOrEmpty(planId))
          return base.BadRequest(ModelState);

        var query = new GetSubscriptionPlanDetailByIdQuery()
        {
          Id = planId
        };

        var result = await _mediator.Send<GetSubscriptionPlanDetailByIdQueryResult>(query);
        return Ok(result);

      }

      ///
      catch (Exception)
      {

        throw;
      }

    }

    [HttpGet("GetAllSubscriptionPlans")]
    [ProducesResponseType(typeof(GetAllSubscriptionPlansQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllSubscriptionPlansQuery()
    {
      var result = await _mediator.Send(new GetAllSubscriptionPlansQuery());
      return Ok(result);
    }


    #endregion


    #region Testing
    //private static Currency GetCurrency(string value)
    //{
    //  return new Currency() { value = value, currency = "USD" };
    //}

    ////public static Plan CreatePlanObject(HttpContext httpContext)
    //private static PayPal.Api.Plan CreatePlanObject(HttpContext httpContext)
    //{
    //  // ### Create the Billing Plan
    //  // Both the trial and standard plans will use the same shipping
    //  // charge for this example, so for simplicity we'll create a
    //  // single object to use with both payment definitions.
    //  var shippingChargeModel = new ChargeModel()
    //  {
    //    type = "SHIPPING",
    //    amount = GetCurrency("9.99")
    //  };

    //  // Define the plan and attach the payment definitions and merchant preferences.
    //  // More Information: https://developer.paypal.com/webapps/developer/docs/api/#create-a-plan
    //  return new PayPal.Api.Plan
    //  {
    //    name = "Basic Plan",
    //    description = "Monthly plan for getting the t-shirt of the month.",
    //    type = "fixed",
    //    // Define the merchant preferences.
    //    // More Information: https://developer.paypal.com/webapps/developer/docs/api/#merchantpreferences-object
    //    merchant_preferences = new MerchantPreferences()
    //    {
    //      setup_fee = GetCurrency("1"),
    //      return_url = "https://localhost:5200",//httpContext.Request.Url.ToString(),
    //      cancel_url = "https://localhost:5200?cancel",//httpContext.Request.Url.ToString() + "?cancel",
    //      auto_bill_amount = "YES",
    //      initial_fail_amount_action = "CONTINUE",
    //      max_fail_attempts = "0"
    //    },
    //    payment_definitions = new List<PaymentDefinition>
    //            {
    //                // Define a trial plan that will only charge $9.99 for the first
    //                // month. After that, the standard plan will take over for the
    //                // remaining 11 months of the year.
    //                new PaymentDefinition()
    //                {

    //                    name = "Trial Plan",
    //                    type = "TRIAL",
    //                    frequency = "MONTH",
    //                    frequency_interval = "1",
    //                    amount = GetCurrency("9.99"),
    //                    cycles = "1",
    //                    charge_models = new List<ChargeModel>
    //                    {
    //                        new ChargeModel()
    //                        {
    //                            type = "TAX",
    //                            amount = GetCurrency("1.65")
    //                        },
    //                        shippingChargeModel
    //                    }
    //                },
    //                // Define the standard payment plan. It will represent a monthly
    //                // plan for $19.99 USD that charges once month for 11 months.
    //                new PaymentDefinition
    //                {
    //                    name = "Standard Plan",
    //                    type = "REGULAR",
    //                    frequency = "MONTH",
    //                    frequency_interval = "1",
    //                    amount = GetCurrency("19.99"),
    //                    // > NOTE: For `IFNINITE` type plans, `cycles` should be 0 for a `REGULAR` `PaymentDefinition` object.
    //                    cycles = "11",
    //                    charge_models = new List<ChargeModel>
    //                    {
    //                        new ChargeModel
    //                        {
    //                            type = "TAX",
    //                            amount = GetCurrency("2.47")
    //                        },
    //                        shippingChargeModel
    //                    }
    //                }
    //            }
    //  };
    //}


    //[HttpPost("CreatePlan")]
    //public void CreatePlan()
    //{
    //  // ### Api Context
    //  // Pass in a `APIContext` object to authenticate 
    //  // the call and to send a unique request id 
    //  // (that ensures idempotency). The SDK generates
    //  // a request id if you do not pass one explicitly. 
    //  // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
    //  var apiContext = PayPalConfiguration.GetAPIContext();

    //  //var plan = CreatePlanObject(HttpContext);
    //  var plan = CreatePlanObject(_contextAccessor.HttpContext);

    //  // ^ Ignore workflow code segment
    //  //#region Track Workflow
    //  //this.flow.AddNewRequest("Create billing plan", plan);
    //  //#endregion

    //  // Call `plan.Create()` to create the billing plan resource.
    //  var createdPlan = plan.Create(apiContext);

    //  var patchRequest = new PatchRequest()
    //  {
    //    new Patch()
    //    {
    //      op = "replace",
    //      path = "/",
    //      value = new Dictionary<string, string>()["state"] = "Active"
    //        //"state": "ACTIVE"
    //      }
    //    };

    //    createdPlan.Update(apiContext, patchRequest);


    //  //// ^ Ignore workflow code segment
    //  //#region Track Workflow
    //  //this.flow.RecordResponse(createdPlan);
    //  //#endregion

    //  // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
    //}


    [HttpPost("CreateSubscriptionPlan")]
    //[ProducesResponseType(typeof(CreateSubscriptionPlanCommandResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateSubscriptionPlanCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSubscriptionPlan([FromBody] CreateSubscriptionPlanCommand command)
    {
      //if (!ModelState.IsValid)
      //  return base.BadRequest(ModelState);

      var result = await _mediator.Send<CreateSubscriptionPlanCommandResult>(command);
      return Ok(result.Payload);
    }



    //This method is called internally after successfully subscribe to a Plan
    [HttpPost("AttachSubscriptionIdToCustomer/{subscriptionId}")]
    [ProducesResponseType(typeof(AttachSubscriptionIdToCustomerCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AttachSubscriptionIdToCustomer([FromRoute] string subscriptionId)
    {
      try
      {
        if (String.IsNullOrEmpty(subscriptionId))
          return base.BadRequest(ModelState);

        var command = new AttachSubscriptionIdToCustomerCommand()
        {
          Id = subscriptionId
        };

        var result = await _mediator.Send<AttachSubscriptionIdToCustomerCommandResult>(command);
        return Ok(result.Payload);
      }
      ///
      catch (Exception)
      {
        throw;
      }

    }


    [HttpPost("CreateProduct")]
    //[ProducesResponseType(typeof(CreateProductCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CreateProductCommandResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
      if (String.Equals("string", command.Type))
        command.Type = "SERVICE";

      if (String.Equals("string", command.Category))
        command.Category = "SOFTWARE";

      try
      {
        if (!ModelState.IsValid)
          return base.BadRequest(ModelState);

        var result = await _mediator.Send<CreateProductCommandResult>(command);
        return Ok(result);
      }
      ///
      catch (InvalidOperationException ex)
      {
        return base.Conflict(ex.Message);
      }

    }


    [HttpGet("GetAllProductDetails")]
    [ProducesResponseType(typeof(GetAllProductDetailsQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllProductDetails()
    {
      var result = await _mediator.Send(new GetAllProductDetailsQuery());
      return Ok(result);
    }

    #endregion


    #region WebHook
    [HttpPost("SubscriptionWebHook")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubscriptionWebHook([FromBody] SubscriptionResponseModel model)
    {
      var Name = model.Resource.subscriber.name.given_name + " " + model.Resource.subscriber.name.surname;
      var Email = model.Resource.subscriber.email_address;
      var LastPayment = model.Resource.billing_info.last_payment.amount.value;
      var LastPaymentDate = model.Resource.billing_info.last_payment.time;

      Console.WriteLine($"Name of subscriber is: {Name}  ");
      Console.WriteLine($"Email of subscriber is: {Email}  ", Email);
      Console.WriteLine($"Last payment of subscription is: {LastPayment}  ", LastPayment);
      Console.WriteLine($"Last payment date of subscription is: {LastPaymentDate}  ", LastPaymentDate);

      return Ok();
      //return "https://www.paypal.com/paypal_webhook_samples/" + Guid.NewGuid().ToString();
    }
    #endregion

  }
}
