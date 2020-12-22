using AutoMapper;
using Fistix.Training.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fistix.Training.Domain.DataModels;
using Fistix.Training.Domain.Commands;
using Fistix.Training.Domain.Dtos;

namespace Fistix.Training.Service.CommandHandlers
{
  public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, TodoDto>
  {
    private readonly ITodoRepository _todoRepository = null;
    private readonly IMapper _mapper = null;
    public UpdateTodoCommandHandler(ITodoRepository todoRepository, IMapper mapper)
    {
      _todoRepository = todoRepository;
      _mapper = mapper;
    }

    public async Task<TodoDto> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
      var entity = _mapper.Map<Todo>(request);
      entity.Id = Guid.Parse(request.Id);
      var uEntity = await _todoRepository.Update(entity);
      await _todoRepository.SaveChanges();
      return _mapper.Map<TodoDto>(uEntity);
    }
  }
}
