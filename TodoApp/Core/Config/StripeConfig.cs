using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Config
{
  public class StripeConfig
  {
    public string SecretKey { get; set; }
    public string PublishableKey { get; set; }
    public string Domain { get; set; }
    public string SuccessUrl { get; set; }
    public string CancelUrl { get; set; }
  }
}
