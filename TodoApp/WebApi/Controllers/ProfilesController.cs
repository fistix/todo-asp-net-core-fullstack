using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Core.Exceptions;
using Fistix.Training.Domain.Commands.MyProfile;
using Fistix.Training.Domain.Commands.Profiles;
using Fistix.Training.Domain.Queries.MyProfile;
using Fistix.Training.Domain.Queries.Profiles;
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
  public class ProfilesController : ControllerBase
  {
    private readonly IMediator _mediator = null;
    private readonly ICurrentUserService _currentUserService = null;

    public ProfilesController(IMediator mediator, ICurrentUserService currentUserService)
    {
      _mediator = mediator;
      _currentUserService = currentUserService;
    }


    [HttpPost]
    [Authorize(Policy = "IsAdmin")]
    [ProducesResponseType(typeof(CreateProfileCommandResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateProfileCommand command)
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
      catch (InvalidOperationException ex)
      {
        return base.Conflict(ex.Message);
      }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateProfileCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProfileCommand command)
    {
      try
      {
        command.Id = id;
        if (!ModelState.IsValid)
        {
          return base.BadRequest(ModelState);
        }

        var result = await _mediator.Send<UpdateProfileCommandResult>(command);
        return base.Ok(result);
      }
      catch (NotFoundException)
      {
        return base.NotFound();
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

        var command = new DeleteProfileCommand()
        {
          Id = id
        };

        var result = await _mediator.Send(command);
        return NoContent();
      }
      catch (NotFoundException)
      {
        return base.NotFound();
      }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetProfileDetailByIdQueryResult), StatusCodes.Status200OK)]
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

        var query = new GetProfileDetailByIdQuery()
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

    [HttpGet("ByEmail")]
    [ProducesResponseType(typeof(GetProfileDetailByEmailQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
      try
      {
        if (!String.IsNullOrWhiteSpace(email))
        {
          return base.BadRequest();
        }

        var query = new GetProfileDetailByEmailQuery() { Email = email };

        var result = await _mediator.Send(query);

        return base.Ok(result);
      }
      catch (NotFoundException)
      {
        return base.NotFound();
      }
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetAllProfilesQueryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
      var result = await _mediator.Send(new GetAllProfilesQuery());
      return base.Ok(result);
    }

    [HttpPut("{id}/ProfilePicture")]
    [ProducesResponseType(typeof(UpdateProfilePictureCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfilePicture([FromRoute] Guid id, [FromForm] UpdateProfilePictureCommand command)
    {
      try
      {
        command.Id = id;

        if (!ModelState.IsValid)
        {
          return base.BadRequest(ModelState);
        }

        var result = await _mediator.Send<UpdateProfilePictureCommandResult>(command);
        return base.Ok(result);
      }
      catch (NotFoundException)
      {
        return base.NotFound();
      }
    }

    [HttpPost("MyProfile")]
    [ProducesResponseType(typeof(CreateMyProfileCommandResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateMyProfile([FromBody] CreateMyProfileCommand command)
    {
      try
      {
        //if (!ModelState.IsValid)
        //{
        //  return base.BadRequest(ModelState);
        //}

        var result = await _mediator.Send<CreateMyProfileCommandResult>(command);
        return base.Created($"api/Profiles/{result.Payload.ProfileId}", result);
      }
      catch (InvalidOperationException ex)
      {
        return base.Conflict(ex.Message);
      }
    }

    [HttpPut("MyProfile")]
    [ProducesResponseType(typeof(UpdateMyProfileCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateMyProfileCommand command)
    {
      try
      {
        //if (!ModelState.IsValid)
        //{
        //  return base.BadRequest(ModelState);
        //}

        var result = await _mediator.Send<UpdateMyProfileCommandResult>(command);
        return base.Ok(result);
      }
      catch (NotFoundException)
      {
        return base.NotFound();
      }
    }


    [HttpGet("MyProfile")]
    [ProducesResponseType(typeof(GetMyProfileDetailByEmailQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMyProfile()
    {
      try
      {

        var query = new GetProfileDetailByEmailQuery() { Email = _currentUserService.Email };

        var result = await _mediator.Send(query);

        return base.Ok(result);
      }
      catch (NotFoundException)
      {
        return base.NotFound();
      }
    }


    [HttpPut("MyProfile/ProfileImage")]
    [ProducesResponseType(typeof(UpdateMyProfilePictureCommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMyProfileImage([FromForm] UpdateMyProfilePictureCommand command)
    {
      try
      {
        
        //if (!ModelState.IsValid)
        //{
        //  return base.BadRequest(ModelState);
        //}

        var result = await _mediator.Send<UpdateMyProfilePictureCommandResult>(command);
        return base.Ok(result);
      }
      catch (NotFoundException)
      {
        return base.NotFound();
      }
    }
  }
}
