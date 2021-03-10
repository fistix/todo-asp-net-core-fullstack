using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class GetAllTransactionsHistoryBySubscriptionIdModel
  {
    public List<Transaction> Transactions { get; set; }
  }
}
