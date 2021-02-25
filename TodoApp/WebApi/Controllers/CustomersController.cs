using Fistix.Training.Core.Exceptions;
using Fistix.Training.Domain.Commands.Customers;
using Fistix.Training.Domain.Queries.Customers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
  [Authorize]
  public class CustomersController : ControllerBase
  {
    private readonly IMediator _mediator = null;

    public CustomersController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCustomerCommandResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
      try
      {
        if (!ModelState.IsValid)
          return base.BadRequest(ModelState);

        var result = await _mediator.Send<CreateCustomerCommandResult>(command);
        return base.Created($"api/Customers/{result.Payload.Id}", result);
        //return Ok(new CreateCustomerCommandResult() { Id = result.Id });
        //return Ok(result);
      }

      catch (InvalidOperationException ex)
      {
        return base.Conflict(ex.Message);
      }

    }


    [HttpGet]
    [ProducesResponseType(typeof(GetCustomerDetailByEmailQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail([FromQuery] GetCustomerDetailByEmailQuery query)
    {
      try
      {
        if (!ModelState.IsValid)
          return base.BadRequest(ModelState);

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
