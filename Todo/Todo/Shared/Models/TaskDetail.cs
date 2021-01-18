using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Shared.Models
{
  public class TaskDetail 
  {
    public Guid? TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public Guid? UserProfileId { get; set; }
  }
}
