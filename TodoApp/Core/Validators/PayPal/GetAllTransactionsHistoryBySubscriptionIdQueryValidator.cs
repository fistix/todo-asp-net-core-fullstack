using Fistix.Training.Domain.Queries.PayPal;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.PayPal
{
  public class GetAllTransactionsHistoryBySubscriptionIdQueryValidator : AbstractValidator<GetAllTransactionsHistoryBySubscriptionIdQuery>
  {
    public GetAllTransactionsHistoryBySubscriptionIdQueryValidator()
    {
      RuleFor(x => x.SubscriptionId).NotEmpty();
      RuleFor(x => x.StartTime).NotEmpty();
      RuleFor(x => x.EndTime).NotEmpty();
    }
  }
}
