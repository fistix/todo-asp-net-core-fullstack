using Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Queries
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
