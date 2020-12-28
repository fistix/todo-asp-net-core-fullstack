using AutoMapper;
using Fistix.Training.Core.Validators.Tasks;
using Fistix.Training.Domain.Commands.Tasks;
using Fistix.Training.Domain.Queries.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
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
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TasksController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTask(CreateTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return base.BadRequest(ModelState);
            }

            var result = await _mediator.Send<CreateTaskCommandResult>(command);

            return base.Created($"api/Tasks/{result.Payload.Id}", result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetAllTasksQueryResult>> GetAll()
        {
            //var result = await _mediator.Send(new GetAllTasksQuery());
            //return result;
            // return base.Ok(await _mediator.Send(new GetAllTasksQueryResult()));
            var result = await _mediator.Send(new GetAllTasksQuery());
            if (result.Payload.Count > 0)
            {
                return base.Ok(result);
            }
            else
            {
                return base.NotFound();
            }
        }


        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<List<Domain.Dtos.TaskDto>>> GetAll()
        //{
        //    //var result = await _mediator.Send(new GetAllTasksQuery());
        //    //return result;
        //    return base.Ok(await _mediator.Send(new GetAllTasksQuery()));
        //}

    }
}
