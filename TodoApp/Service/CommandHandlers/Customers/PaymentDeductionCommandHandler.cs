using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Customers;
using Fistix.Training.Domain.Dtos;
using MediatR;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.Customers
{
  public class PaymentDeductionCommandHandler : IRequestHandler<PaymentDeductionCommand, PaymentDeductionCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly ICustomerRepository _customerRepository = null;

    private readonly StripeService _stripeService = null;

    public PaymentDeductionCommandHandler(IMapper mapper, ICustomerRepository customerRepository, StripeService stripeService)
    {
      _mapper = mapper;
      _customerRepository = customerRepository;
      _stripeService = stripeService;
    }
    public async Task<PaymentDeductionCommandResult> Handle(PaymentDeductionCommand command, CancellationToken cancellationToken)
    {
      var customer = await _customerRepository.GetById(Guid.Parse(command.CustomerId));

      var paymentIntent = await _stripeService.PaymentDeduct(command);

      customer.Amount = command.Amount;
      customer.LastPaymentDeduct = DateTime.Now;

      //var updatedCustomer = _mapper.Map(command, customer);

      var response = await _customerRepository.Update(customer);
      if (response)
      {
        return new PaymentDeductionCommandResult()
        {
          Payload = _mapper.Map<CustomerDto>(customer)
        };
      }

      else
      {
        return new PaymentDeductionCommandResult()
        {
          Payload = null
        };
      }

    }
  }
}
