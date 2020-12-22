using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.DataModels
{
  public class Entity
  {
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
  }
}
