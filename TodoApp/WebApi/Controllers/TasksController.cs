using Domain.Commands.Tasks;
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
  public class TasksController : ControllerBase
  {
    private IMediator _mediator;

    public TasksController(IMediator mediator)
    { 
      _mediator = mediator;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTask(CreateTaskCommand command)
    {
      if(!ModelState.IsValid)
        return base.BadRequest(ModelState);
      
      var result = await _mediator.Send<CreateTaskCommandResult>(command);

      var taskId = result.Payload.Id;
      return base.Created($"api/Tasks/{taskId}", null);
    }
  }
}
