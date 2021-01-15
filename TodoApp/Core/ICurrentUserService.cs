using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core
{
  public interface ICurrentUserService
  {
    public string Email { get; set; }
  }
}
