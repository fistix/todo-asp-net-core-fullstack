using Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Queries
{
  public class FetchAllTodoQuery : IRequest<List<TodoDto>>
  {
  }
}
