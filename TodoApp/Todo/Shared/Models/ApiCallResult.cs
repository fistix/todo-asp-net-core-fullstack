using Fistix.Training.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Shared.Models
{
  public class ApiCallResult<T>
  {
    public string Operation { get; set; }
    public bool IsSucceed { get; set; }
    public string ErrorMessage { get; set; }
    public T Data { get; set; }

    public CustomerDto Payload { get; set; }
  }
}
