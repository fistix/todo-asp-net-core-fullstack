using Fistix.Training.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Queries.Tasks
{
  public class GetMyAllTasksQueryResult
  {
    public List<TaskDto> Payload { get; set; }
  }
}
