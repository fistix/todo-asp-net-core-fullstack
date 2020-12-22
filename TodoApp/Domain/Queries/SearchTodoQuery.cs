using Fistix.Training.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Queries
{
  public class SearchTodoQuery : IRequest<List<TodoDto>>
  {
    public SearchTodoQuery(string term)
    {
      Term = term;
    }
    public string Term { get; set; }
  }
}
