using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Paypal;
using Fistix.Training.Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.PayPal
{
  public class AttachSubscriptionIdToCustomerCommandHandler : IRequestHandler<AttachSubscriptionIdToCustomerCommand, AttachSubscriptionIdToCustomerCommandResult>
  {
    private readonly PayPalService _payPalService = null;
    private readonly ICustomerRepository _customerRepository = null;
    public AttachSubscriptionIdToCustomerCommandHandler(PayPalService payPalService, ICustomerRepository customerRepository)
    {
      _payPalService = payPalService;
      _customerRepository = customerRepository;
    }

    public async Task<AttachSubscriptionIdToCustomerCommandResult> Handle(AttachSubscriptionIdToCustomerCommand command, CancellationToken cancellationToken)
    {
      var response = false;

      var result = await _payPalService.GetSubscriptionDetailbyId(command.Id);
      var customer = await _customerRepository.GetByEmail(result.subscriber.email_address);

      if (customer != null)
      {
        customer.PayPalSubscriptionId = result.Id;
        customer.PayPalLastPaymentDeduct = result.billing_info.last_payment.amount.value;
        customer.PayPalLastPaymentDeductOn = result.billing_info.last_payment.time;
        response = await _customerRepository.Update(customer);
      }

      else
      {
        //Gets error if we use "customer" object
        Customer newCustomer = new Customer();

        newCustomer.Id = Guid.NewGuid();
        newCustomer.Email = result.subscriber.email_address;
        newCustomer.FirstName = result.subscriber.name.given_name;
        newCustomer.LastName = result.subscriber.name.surname;
        newCustomer.PayPalSubscriptionId = result.Id;
        newCustomer.PayPalCustomerName = result.subscriber.name.given_name + " " + result.subscriber.name.surname;
        newCustomer.PayPalSubscriptionId = result.Id;
        newCustomer.PayPalLastPaymentDeduct = result.billing_info.last_payment.amount.value;
        newCustomer.PayPalLastPaymentDeductOn = result.billing_info.last_payment.time;
        response = await _customerRepository.Create(newCustomer);
      }

      return new AttachSubscriptionIdToCustomerCommandResult()
      {
        Payload = result
        //IsSucceed = response
      };

    }
  }
}
