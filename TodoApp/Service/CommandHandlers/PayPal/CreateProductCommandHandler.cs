using Fistix.Training.Domain.Commands.Paypal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.PayPal
{
  public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResult>
  {
    private readonly PayPalService _payPalService = null;
    public CreateProductCommandHandler(PayPalService payPalService)
    {
      _payPalService = payPalService;
    }
    public async Task<CreateProductCommandResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
      //var products = await _payPalService.GetAllProductDetails();

      //if (products.Products.Any(x => x.Name.Equals(command.Name)))
      //  throw new InvalidOperationException("Product with same Name already exist!");

      //if (products.Products.Any(x => x.Id.Equals(command.Id)))
      //  throw new InvalidOperationException("Product with same Id already exist!");

      var product = await _payPalService.CreateProduct(command);

      return new CreateProductCommandResult()
      {
        Payload = product
      };
    }
  }
}
