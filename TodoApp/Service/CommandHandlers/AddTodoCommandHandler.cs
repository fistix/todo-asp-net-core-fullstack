﻿using AutoMapper;
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
  public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, TodoDto>
  {
    private readonly ITodoRepository _todoRepository = null;
    private readonly IMapper _mapper = null;
    public AddTodoCommandHandler(ITodoRepository todoRepository, IMapper mapper)
    {
      _todoRepository = todoRepository;
      _mapper = mapper;
    }

    public async Task<TodoDto> Handle(AddTodoCommand request, CancellationToken cancellationToken)
    {
      var todo = _mapper.Map<Todo>(request);
      var entity = await _todoRepository.Add(todo);
      await _todoRepository.SaveChanges();
      return _mapper.Map<TodoDto>(entity);
    }
  }
}
