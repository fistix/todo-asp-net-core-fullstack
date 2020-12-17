using AutoMapper;
using Domain.Commands;
using Domain.Dtos;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
  public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      CreateMap<Todo, TodoDto>()
        .ForMember(x => x.Id, v => v.MapFrom(z => z.Id.ToString()));
      CreateMap<TodoDto, Todo>()
        .ForMember(x => x.Id, v => v.MapFrom(z => Guid.Parse(z.Id)));

      CreateMap<AddTodoCommand, Todo>();
      CreateMap<Todo, AddTodoCommand>();

      CreateMap<UpdateTodoCommand, Todo>();
      CreateMap<Todo, UpdateTodoCommand>();

      CreateMap<UpdateTodoCommand, TodoDto>();
      CreateMap<TodoDto, UpdateTodoCommand>();

      CreateMap<AddTodoCommand, TodoDto>();
      CreateMap<TodoDto, AddTodoCommand>();

    }
  }
}
