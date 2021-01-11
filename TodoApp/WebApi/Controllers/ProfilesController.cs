using AutoMapper;
using Fistix.Training.Core.Exceptions;
using Fistix.Training.Domain.Commands.Profiles;
using Fistix.Training.Domain.Queries.Profiles;
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
    public class ProfilesController : ControllerBase
    {
        private readonly IMapper _mapper = null;
        private readonly IMediator _mediator = null;

        public ProfilesController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateProfileCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                }
                var result = await _mediator.Send<CreateProfileCommandResult>(command);
                return base.Created($"api/Profiles/{result.Payload.ProfileId}", result);
            }
            catch (Exception ex)
            {
                //return base.BadRequest(ex.Message);
                return base.Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProfileCommand command)
        {
            try
            {
                //command.Id = id;
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                }
                var result = await _mediator.Send<UpdateProfileCommandResult>(command);
                return base.Ok(result);
            }
            catch (NotFoundException nfx)
            {
                return base.NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
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
                var result = await _mediator.Send(new DeleteProfileCommand() { Id = id });
                return base.Ok("Successfully Deleted!");
            }
            catch (NotFoundException)
            {
                return base.NotFound();
            }
            catch (Exception)
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
                }
                var result = await _mediator.Send(new GetProfileDetailByIdQuery() { Id = id });
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

        [HttpGet("ByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                }
                var result = await _mediator.Send(new GetProfileDetailByEmailQuery() { Email = email });
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
            var result = await _mediator.Send(new GetAllProfilesQuery());
            return base.Ok(result);
        }

        [HttpPut("{id}/ProfilePicture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProfilePicture([FromRoute] Guid id, [FromForm] UpdateProfilePictureCommand command)
        {
            try
            {
                var result = await _mediator.Send<UpdateProfilePictureCommandResult>(command);
                return base.Ok(result);
            }
            catch (NotFoundException)
            {
                return base.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
