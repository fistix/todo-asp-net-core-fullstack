using Fistix.Training.Core.Exceptions;
using Fistix.Training.Domain.Commands.Customers;
using Fistix.Training.Domain.Queries.Stripe;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomersController : ControllerBase
  {
    private readonly IMediator _mediator = null;

    public CustomersController(IMediator mediator)
    {
      _mediator = mediator;
    }

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

        return base.Created($"api/Customers/Create/{result.Payload.Id}", result);
        //return Ok(new CreateCustomerCommandResult() { Id = result.Id });
        //return Ok(result);
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

  }
}
