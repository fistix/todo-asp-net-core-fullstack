using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Paypal
{
  public class CreateProductCommand : IRequest<CreateProductCommandResult>
  {
    //System will generate Product Id if it is not provided
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    //Types
    //"PHYSICAL" Physical goods.
    //"DIGITAL" Digital goods.
    //"SERVICE" A service. For example, technical support
    public string Type { get; set; } = "SERVICE";

    //Long list
    //SOFTWARE
    public string Category { get; set; } = "SOFTWARE";
  }
}
