using AutoMapper;
using Fistix.Training.Core.Exceptions;
using Fistix.Training.Core.Validators.Tasks;
using Fistix.Training.Domain.Commands.Tasks;
using Fistix.Training.Domain.Queries.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
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
  public class TasksController : ControllerBase
  {
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
      _mediator = mediator;
    }


    [HttpPost]
    [ProducesResponseType(typeof(CreateTaskCommandResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return base.BadRequest(ModelState);
        }

        var result = await _mediator.Send<CreateTaskCommandResult>(command);
        return base.Created($"api/Tasks/{result.Payload.Id}", result);
      }
      catch (ArgumentException ex)
      {
        return base.BadRequest(ex.Message);
      }
    }


    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateTaskCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status304NotModified)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskCommand command)
    {
      try
      {
        command.Id = id;
        if (!ModelState.IsValid)
        {
          return base.BadRequest(ModelState);
        }

        var result = await _mediator.Send<UpdateTaskCommandResult>(command);
        return base.Ok(result);
      }
      catch (NotFoundException nfx)
      {
        return base.NotFound(nfx.Message);
      }
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
      try
      {
        if (id.Equals(Guid.Empty))
        {
          return base.BadRequest();
        }

        var command = new DeleteTaskCommand()
        {
          Id = id
        };

        var result = await _mediator.Send(command);
        return base.NoContent();
      }
      catch (NotFoundException)
      {
        return base.NotFound();
        //return base.NotFound($"Id {id} not found!");
      }
    }


    [HttpGet]
    [ProducesResponseType(typeof(GetAllTasksQueryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
      var result = await _mediator.Send(new GetAllTasksQuery());
      return base.Ok(result);

      //if (result.Payload.Count > 0)
      //{
      //    return base.Ok(result);
      //}
      //else
      //{
      //    return base.NotFound();
      //}
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetTaskDetailByIdQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
      try
      {
        if (id.Equals(Guid.Empty))
        {
          return base.BadRequest();
        }

        var query = new GetTaskDetailByIdQuery()
        {
          Id = id
        };

        var result = await _mediator.Send(query);
        return base.Ok(result);
      }
      catch (NotFoundException)
      {
        return base.NotFound();
      }
    }


    [HttpPut("{id}/AssignUser")]
    [ProducesResponseType(typeof(AttachUserWithTaskCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignUser([FromRoute] Guid id, [FromBody] AttachUserWithTaskCommand command/*,[FromRoute] Guid profileId*/)
    {
      try
      {
        command.TaskId = id;
        if (!ModelState.IsValid)
        {
          return base.BadRequest(ModelState);
        }

        //var result = await _mediator.Send(new AttachUserWithTaskCommand() { TaskId = taskId/*, UserId = profileId*/ });
        //var result = await _mediator.Send(new AttachUserWithTaskCommandResult());
        var result = await _mediator.Send<AttachUserWithTaskCommandResult>(command);
        return base.Ok(result);
      }
      catch (NotFoundException)
      {
        return base.NotFound();
      }
    }


    [HttpPut("{id}/UnAssignUser")]
    [ProducesResponseType(typeof(UnAttachUserWithTaskCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnAssignUser([FromRoute] Guid id, [FromBody] UnAttachUserWithTaskCommand command)
    {
      try
      {
        command.TaskId = id;
        if (!ModelState.IsValid)
        {
          return base.BadRequest(ModelState);
        }
        var result = await _mediator.Send<UnAttachUserWithTaskCommandResult>(command);
        return base.Ok(result);
      }
      catch (NotFoundException nfx)
      {
        return base.NotFound(nfx.Message);
      }
    }


    [HttpPost("MyTask")]
    [ProducesResponseType(typeof(CreateMyTaskCommandResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMyTask([FromBody] CreateMyTaskCommand command)
    {
      try
      {
        //if (!ModelState.IsValid)
        //{
        //  return base.BadRequest(ModelState);
        //}

        var result = await _mediator.Send<CreateMyTaskCommandResult>(command);
        return base.Created($"api/Tasks/{result.Payload.Id}", result);
      }
      catch (ArgumentException ex)
      {
        return base.BadRequest(ex.Message);
      }
    }


    [HttpPut("MyTask/{id}")]
    [ProducesResponseType(typeof(UpdateMyTaskCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status304NotModified)]
    public async Task<IActionResult> UpdateMyTask([FromRoute] Guid id, [FromBody] UpdateMyTaskCommand command)
    {
      try
      {
        command.Id = id;
        //if (!ModelState.IsValid)
        //{
        //  return base.BadRequest(ModelState);
        //}

        var result = await _mediator.Send<UpdateMyTaskCommandResult>(command);
        return base.Ok(result);
      }
      catch (NotFoundException nfx)
      {
        return base.NotFound(nfx.Message);
      }
      catch (InvalidOperationException)
      {
        return base.Conflict();
      }
    }


    [HttpGet("MyTask")]
    [ProducesResponseType(typeof(GetMyAllTasksQueryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyAllTasks()
    {
      try
      {
        var result = await _mediator.Send(new GetMyAllTasksQuery());
        return base.Ok(result);
      }
      catch (NotFoundException nfx)
      {
        return base.NotFound(nfx.Message);
      }
    }


    [HttpGet("MyTask/{id}")]
    [ProducesResponseType(typeof(GetMyTaskDetailQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMyTaskById([FromRoute] Guid id)
    {
      try
      {
        if (id.Equals(Guid.Empty))
        {
          return base.BadRequest();
        }

        var query = new GetMyTaskDetailQuery()
        {
          Id = id
        };

        var result = await _mediator.Send(query);
        return base.Ok(result);
      }
      catch (NotFoundException nfx)
      {
        return base.NotFound(nfx.Message);
      }
      catch (InvalidOperationException ix)
      {
        return base.Conflict(ix.Message);
      }
    }
  
  }
}