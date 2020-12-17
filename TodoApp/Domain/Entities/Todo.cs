using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
  public class Todo : Entity
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
  }
}
