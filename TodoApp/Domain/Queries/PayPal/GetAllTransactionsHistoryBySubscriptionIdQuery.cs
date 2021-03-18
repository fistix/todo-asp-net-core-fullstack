using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Fistix.Training.Domain.Queries.PayPal
{
  public class GetAllTransactionsHistoryBySubscriptionIdQuery : IRequest<GetAllTransactionsHistoryBySubscriptionIdQueryResult>
  {
    [JsonIgnore]
    public string SubscriptionId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
  }
}
