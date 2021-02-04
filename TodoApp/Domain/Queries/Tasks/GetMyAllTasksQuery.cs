using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Queries.Tasks
{
  public class GetMyAllTasksQuery : IRequest<GetMyAllTasksQueryResult>
  {
  }
}
