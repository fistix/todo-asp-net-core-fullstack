using AutoMapper;
using Core;
using Domain.Dtos;
using Domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.QueryHandlers
{
  public class FetchAllTodoQueryHandler : IRequestHandler<FetchAllTodoQuery, List<TodoDto>>
  {
    private readonly ITodoRepository _todoRepository = null;
    private readonly IMapper _mapper = null;
    public FetchAllTodoQueryHandler(ITodoRepository todoRepository, IMapper mapper)
    {
      _todoRepository = todoRepository;
      _mapper = mapper;
    }

    public async Task<List<TodoDto>> Handle(FetchAllTodoQuery request, CancellationToken cancellationToken)
    {
      try
      {
        return _mapper.Map<List<TodoDto>>(await _todoRepository.Get());
      }
      catch(Exception ex)
      {
        throw;
      }
    }
  }
}
