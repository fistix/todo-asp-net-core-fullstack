using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.DataModels
{
    public class Task /*: Entity*/
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
