using AutoMapper;
using Fistix.Training.Domain.Commands;
using Fistix.Training.Domain.Commands.Profiles;
using Fistix.Training.Domain.Commands.Tasks;
using Fistix.Training.Domain.DataModels;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Service
{
  public class MapperProfile : AutoMapper.Profile
  {
    public MapperProfile()
    {

      CreateMap<Task, TaskDto>();
      CreateMap<TaskDto, Task>();

      CreateMap<CreateTaskCommand, Task>();
      CreateMap<Task, CreateTaskCommand>();

      CreateMap<UpdateTaskCommand, Task>()
          .ForMember(x => x.CreatedOn, v => v.UseDestinationValue())
          .ForMember(x => x.UserProfileId, v => v.UseDestinationValue());
      CreateMap<Task, UpdateTaskCommand>();


      CreateMap<Domain.DataModels.Profile, ProfileDto>();
      CreateMap<ProfileDto, Domain.DataModels.Profile>();

      CreateMap<CreateProfileCommand, Domain.DataModels.Profile>();
      CreateMap<Domain.DataModels.Profile, CreateProfileCommand>();

      CreateMap<UpdateProfileCommand, Domain.DataModels.Profile>()
          .ForMember(x => x.ProfileId, v => v.UseDestinationValue())
          .ForMember(x => x.Email, v => v.UseDestinationValue())
          .ForMember(x => x.ProfilePictureUrl, v => v.UseDestinationValue());
      CreateMap<Domain.DataModels.Profile, UpdateProfileCommand>();



      ////
      CreateMap<AttachUserWithTaskCommand, Task>()
          .ForMember(x => x.Title, v => v.Ignore())
          .ForMember(x => x.Description, v => v.Ignore())
          .ForMember(x => x.Active, v => v.Ignore())
          .ForMember(x => x.CreatedOn, v => v.Ignore())
          .ForMember(x => x.ModifiedOn, v => v.Ignore());
      CreateMap<Task, AttachUserWithTaskCommand>();

      CreateMap<AttachUserWithTaskCommand, Domain.DataModels.Profile>();
      CreateMap<Domain.DataModels.Profile, AttachUserWithTaskCommand>();


    }
  }
}
