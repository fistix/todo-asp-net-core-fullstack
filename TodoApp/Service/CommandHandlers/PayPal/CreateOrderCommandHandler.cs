using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Paypal;
using Fistix.Training.Domain.PayPalModels;
using MediatR;
using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.PayPal
{
  public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResult>
  {
    private readonly IMapper _mapper = null;
    //private readonly ICustomerRepository _customerRepository = null;
    private readonly PayPalService _payPalService = null;

    public CreateOrderCommandHandler(IMapper mapper,/* ICustomerRepository customerRepository,*/ PayPalService payPalService)
    {
      _mapper = mapper;
      //_customerRepository = customerRepository;
      _payPalService = payPalService;
    }

    public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
      var order = await _payPalService.CreateOrder(command);
      return new CreateOrderCommandResult()
      {
        Payload = _mapper.Map<OrderModel>(order)
        //Order = order
      };
      
    }
  }
}
