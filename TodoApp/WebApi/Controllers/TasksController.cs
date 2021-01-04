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

        //// Changings are pending
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] UpdateTaskCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                }
                var result = await _mediator.Send<UpdateTaskCommandResult>(command);
                if (result != null)
                {
                    return base.Ok(result);
                }
                return base.NotFound();
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                    //return base.BadRequest(id);
                    //throw new ArgumentException("Id is empty");
                }
                var result = await _mediator.Send(new GetTaskDetailByIdQuery() { Id = id });
                return base.Ok(result);
            }
            catch (Exception)
            {
                return base.NotFound();
                //throw;
            }
        }

        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask([FromRoute]Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    //return base.BadRequest($"{id} is empty!");
                    return base.BadRequest("Id is empty!");
                }
                var result = await _mediator.Send(new DeleteTaskCommand() { Id = id });

                return base.Ok("Successfully Deleted!");
            }
            catch (ArgumentException)
            {
                return base.NotFound($"Id {id} not found!");
            }
            //Sir code review of previous tasks is also remaining
        }

    }
}
