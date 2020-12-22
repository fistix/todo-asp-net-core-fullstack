using AutoMapper;
using Fistix.Training.Domain.Commands;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fistix.Training.TodoApp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize]
  public class TodoController : ControllerBase
  {
    private readonly ILogger<TodoController> _logger = null;
    private readonly IMediator _mediator = null;
    private readonly IMapper _mapper = null;

    public TodoController(IMediator mediator, IMapper mapper, ILogger<TodoController> logger)
    {
      _mediator = mediator;
      _mapper = mapper;
      _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> Add([FromBody]AddTodoCommand command)
    {
      try
      {
        return Ok(await _mediator.Send(command));
      }
      catch(Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    [HttpPut]
    public async Task<ActionResult<TodoDto>> Update([FromBody] UpdateTodoCommand command)
    {
      try
      {
        return Ok(await _mediator.Send(command));
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete([FromQuery] string id)
    {
      try
      {
        return Ok(await _mediator.Send(new DeleteTodoCommand(id)));
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    [HttpGet("Search/{term}")]
    public async Task<ActionResult<List<TodoDto>>> Search([FromRoute] string term)
    {
      try
      {
        return Ok(await _mediator.Send(new SearchTodoQuery(term)));
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoDto>>> Get()
    {
      try
      {
        return Ok(await _mediator.Send(new FetchAllTodoQuery()));
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<bool>> Get([FromRoute]string id)
    {
      try
      {
        return Ok(await _mediator.Send(new GetTodoByIdQuery(id)));
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

  }
}
