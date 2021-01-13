using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Queries.Tasks
{
  public class GetAllTasksQuery : IRequest<GetAllTasksQueryResult>
  {
  }
}
