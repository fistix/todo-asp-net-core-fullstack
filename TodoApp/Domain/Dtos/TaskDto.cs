using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Dtos
{
  public class TaskDto
  {
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public Guid? UserId { get; set; }
  }
}
