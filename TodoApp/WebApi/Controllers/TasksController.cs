using AutoMapper;
using Fistix.Training.Core.Exceptions;
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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TasksController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                }

                var result = await _mediator.Send<CreateTaskCommandResult>(command);
                return base.Created($"api/Tasks/{result.Payload.TaskId}", result);
            }
            catch (ArgumentException ex)
            {
                return base.BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                }
                //var result = await _mediator.Send<UpdateTaskCommandResult>(command);
                var result = await _mediator.Send<UpdateTaskCommandResult>(command);
                return base.Ok(result);
            }
            catch (NotFoundException nfx)
            {
                return base.NotFound(nfx.Message);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                }
                var result = await _mediator.Send(new DeleteTaskCommand() { Id = id });
                return base.Ok("Successfully Deleted!");
            }
            catch (NotFoundException)
            {
                return base.NotFound();
                //return base.NotFound($"Id {id} not found!");
            }
            catch (ArgumentException)
            {
                throw;
            }
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
            catch (NotFoundException nfx)
            {
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

        [HttpPut("{id}/AssignUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignUser([FromRoute] Guid id, [FromBody] AttachUserWithTaskCommand command/*,[FromRoute] Guid profileId*/)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                }
                //var result = await _mediator.Send(new AttachUserWithTaskCommand() { TaskId = taskId/*, UserId = profileId*/ });
                //var result = await _mediator.Send(new AttachUserWithTaskCommandResult());
                var result = await _mediator.Send<AttachUserWithTaskCommandResult>(command);

                return base.Ok(result);
            }
            catch (NotFoundException nfx)
            {
                return base.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}/UnAssignUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<IActionResult> UnAssignUser([FromRoute] Guid id, [FromBody] UnAttachUserWithTaskCommand command)
        {
            try
            {
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
