using Fistix.Training.Domain.Commands.Paypal;
using Fistix.Training.Domain.Queries.PayPal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  //[Authorize]
  public class PayPalController : ControllerBase
  {
    private readonly IMediator _mediator = null;
    private readonly HttpClient _httpClient = null;


    public PayPalController(IMediator mediator, HttpClient httpClient)
    {
      _mediator = mediator;
      _httpClient = httpClient;
    }

    #region OneTimeCheckout

    [HttpPost]
    [ProducesResponseType(typeof(CreateOrderCommandResult), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOrder()
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

    [HttpPost("CaptureOrder")]
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

    [HttpGet("GetAllTransactions")]
    [ProducesResponseType(typeof(GetAllTransactionsHistoryBySubscriptionIdQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllTransactionsHistoryBySubscriptionId([FromQuery] string Id, [FromBody] GetAllTransactionsHistoryBySubscriptionIdQuery query)
    {
      try
      {
        Id = "I-SX5SXUFJH4KW";
        query.Id = Id;

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


    [HttpGet("GetSubscriptionPlanDetailById/{id}")]
    [ProducesResponseType(typeof(GetSubscriptionPlanDetailByIdQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSubscriptionPlanDetailById([FromRoute] string id/*, [FromBody] GetSubscriptionPlanDetailByIdQuery query*/)
    {
      try
      {
        id = "P-1GP34431BG4466351MBAOLLA";
        //query.Id = Id;

        //if (!ModelState.IsValid)
        if (String.IsNullOrEmpty(id))
          return base.BadRequest(ModelState);

        var query = new GetSubscriptionPlanDetailByIdQuery()
        {
          Id = id
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

  }
}
