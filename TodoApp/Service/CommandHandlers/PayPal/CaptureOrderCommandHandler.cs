using AutoMapper;
using Fistix.Training.Domain.Commands.Paypal;
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
    public CaptureOrderCommandHandler(IMapper mapper, PayPalService  payPalService)
    {
      _mapper = mapper;
      _payPalService = payPalService;
    }
    public async Task<CaptureOrderCommandResult> Handle(CaptureOrderCommand command, CancellationToken cancellationToken)
    {
      var order = await _payPalService.CaptureOrder(command.OrderId);

      return new CaptureOrderCommandResult()
      {
        //Order = _mapper.Map<OrderModel>(order)
        Order = order
      };
      //throw new NotImplementedException();
    }
  }
}
