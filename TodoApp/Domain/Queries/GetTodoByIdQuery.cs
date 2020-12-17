using Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Queries
{
  public class GetTodoByIdQuery : IRequest<TodoDto>
  {
    public GetTodoByIdQuery(string id)
    {
      Id = id;
    }

    public string Id { get; set; }
  }
}
