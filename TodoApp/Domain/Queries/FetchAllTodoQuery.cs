using Fistix.Training.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Queries
{
  public class FetchAllTodoQuery : IRequest<List<TodoDto>>
  {
  }
}
