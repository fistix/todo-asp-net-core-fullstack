using AutoMapper;
using Core;
using Domain.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.CommandHandlers
{
  public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
  {
    private readonly ITodoRepository _todoRepository = null;
    private readonly IMapper _mapper = null;
    public DeleteTodoCommandHandler(ITodoRepository todoRepository, IMapper mapper)
    {
      _todoRepository = todoRepository;
      _mapper = mapper;
    }
    public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
      try
      {
        var response = await _todoRepository.Delete(Guid.Parse(request.Id));
        await _todoRepository.SaveChanges();
        return response;
      }
      catch(ArgumentException ix)
      {
        throw;
      }
      catch(Exception ex)
      {
        throw;
      }
    }
  }
}
