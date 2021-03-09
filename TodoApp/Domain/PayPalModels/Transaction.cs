using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class Transaction
  {
    public string status { get; set; }
    public string id { get; set; }
    public AmountWithBreakdown amount_with_breakdown { get; set; }
    public PayerName payer_name { get; set; }
    public string payer_email { get; set; }
    public DateTime time { get; set; }
  }
}
