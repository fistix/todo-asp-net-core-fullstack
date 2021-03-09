using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class PaymentPreferences
  {
    public string service_type { get; set; }
    public bool auto_bill_outstanding { get; set; }
    public SetupFee setup_fee { get; set; }
    public string setup_fee_failure_action { get; set; }
    public int payment_failure_threshold { get; set; }
  }
}
