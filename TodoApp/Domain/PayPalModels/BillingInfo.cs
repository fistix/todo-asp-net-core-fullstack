using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class BillingInfo
  {
    public OutstandingBalance outstanding_balance { get; set; }
    public List<CycleExecution> cycle_executions { get; set; }
    public LastPayment last_payment { get; set; }
    public DateTime next_billing_time { get; set; }
    public DateTime final_payment_time { get; set; }
    public int failed_payments_count { get; set; }
  }
}
