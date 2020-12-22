using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.QueryHandlers
{
  public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoDto>
  {
    private readonly ITodoRepository _todoRepository = null;
    private readonly IMapper _mapper = null;
    public GetTodoByIdQueryHandler(ITodoRepository todoRepository, IMapper mapper)
    {
      _todoRepository = todoRepository;
      _mapper = mapper;
    }
    public async Task<TodoDto> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
      try
      {
        return _mapper.Map<TodoDto>(await _todoRepository.Get(Guid.Parse(request.Id)));
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
