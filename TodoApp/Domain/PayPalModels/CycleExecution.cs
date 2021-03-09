using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class CycleExecution
  {
    public string tenure_type { get; set; }
    public int sequence { get; set; }
    public int cycles_completed { get; set; }
    public int cycles_remaining { get; set; }
    public int current_pricing_scheme_version { get; set; }
  }
}
