using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Config
{
  public class Auth0Config
  {
    public string Audience { get; set; }
    public string Domain { get; set; }
    public string ClientSecret { get; set; }
    public string ClientId { get; set; }
  }
}
