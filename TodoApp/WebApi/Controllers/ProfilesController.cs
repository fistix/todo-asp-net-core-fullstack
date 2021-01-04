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
        private readonly IMediator _mediator = null;
        private readonly IMapper _mapper = null;

        public ProfilesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateProfileCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                }
                var result = await _mediator.Send<CreateProfileCommandResult>(command);
                return base.Created($"api/Profiles/{result.Payload.Id}", result);
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
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody]UpdateProfileCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return base.BadRequest(ModelState);
                }
                var result = await _mediator.Send<UpdateProfileCommandResult>(command);
                return base.Ok(result);
            }
            catch (NotFoundException nfx)
            {
                return base.NotFound(nfx.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProfilesQuery());
            return base.Ok(result);
        }

        [HttpGet("ByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery]string email)
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
            catch(NotFoundException nfx)
            {
                return base.NotFound(nfx.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute]Guid id)
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
                return base.NotFound(nfx.Message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
