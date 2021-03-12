using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Paypal;
using Fistix.Training.Domain.DataModels;
using Fistix.Training.Domain.PayPalModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.PayPal
{
  public class CaptureOrderCommandHandler : IRequestHandler<CaptureOrderCommand, CaptureOrderCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly PayPalService _payPalService = null;
    private readonly ICustomerRepository _customerRepository = null;
    public CaptureOrderCommandHandler(IMapper mapper, PayPalService payPalService, ICustomerRepository customerRepository)
    {
      _mapper = mapper;
      _payPalService = payPalService;
      _customerRepository = customerRepository;
    }
    public async Task<CaptureOrderCommandResult> Handle(CaptureOrderCommand command, CancellationToken cancellationToken)
    {
      //var order = await _payPalService.CaptureOrder(command.OrderId);

      //return new CaptureOrderCommandResult()
      //{
      //  //Order = _mapper.Map<OrderModel>(order)
      //  Order = order
      //};
      ////throw new NotImplementedException();
      ///

      var order = await _payPalService.CaptureOrder(command.OrderId);

      Customer cust = new Customer();
      var response = false;

      var customer = await _customerRepository.GetByEmail(order.Payer.Email);

      if (customer == null/* && String.IsNullOrEmpty(customer.PayPalCustomerEmail)*/)
      {
        cust.Id = Guid.NewGuid();
        cust.FirstName = order.Payer.Name.GivenName;
        cust.LastName = order.Payer.Name.Surname;
        cust.Email = order.Payer.Email;

        cust.PayPalCustomerId = order.Payer.PayerId;
        cust.PayPalCustomerName = order.Payer.Name.GivenName + " " + order.Payer.Name.Surname;
        cust.PayPalLastPaymentDeduct = order.PurchaseUnits[0].AmountWithBreakdown.Value;
        cust.PayPalLastPaymentDeductOn = DateTime.Parse(order.CreateTime);

        response = await _customerRepository.Create(cust);
      }

      else
      {
        //if(String.IsNullOrEmpty(customer.Email/*.PayPalCustomerEmail*/))
        //{
          customer.PayPalCustomerId = order.Payer.PayerId;
          customer.PayPalCustomerName = order.Payer.Name.GivenName + " " + order.Payer.Name.Surname;
          customer.PayPalLastPaymentDeduct = order.PurchaseUnits[0].AmountWithBreakdown.Value;
          customer.PayPalLastPaymentDeductOn = DateTime.Parse(order.CreateTime);
          //customer.PayPalLastPaymentDeductOn = DateTime.Parse(order.UpdateTime);
        //}
        //var r = !String.IsNullOrEmpty(customer.Email/*.PayPalCustomerEmail*/);
        response = await _customerRepository.Update(customer);
      }

      if (response)
      {
        return new CaptureOrderCommandResult()
        {
          //Order = _mapper.Map<OrderModel>(order)
          Order = order
        };
      }

      else
      {
        return new CaptureOrderCommandResult()
        {
          Order = null
        };
      }

    }

    private void SetCustomeProperties()
    {

    }

  }

}

