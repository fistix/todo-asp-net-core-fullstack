using AutoMapper;
using Fistix.Training.Domain.Commands.Profiles;
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

        public ProfilesController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateProfileCommand command)
        {
            if (!ModelState.IsValid)
            {
                return base.BadRequest(ModelState);
            }
            var result = await _mediator.Send<CreateProfileCommandResult>(command);
            return base.Created($"api/Profiles/{result.Payload.Id}", result);
        }
    }
}
