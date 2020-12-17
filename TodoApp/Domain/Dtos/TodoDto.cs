using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
  public class TodoDto
  {
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
  }
}
