using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Shared.Models
{
  public class ResponseModel
  {
    public List<TaskDetail> Payload { get; set; }
  }
}
