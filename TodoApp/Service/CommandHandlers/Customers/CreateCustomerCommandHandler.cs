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
  public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResult>
  {

    private readonly IMapper _mapper = null;
    private readonly ICustomerRepository _customerRepository = null;
    private readonly StripeService _stripeService = null;
    public CreateCustomerCommandHandler(IMapper mapper, ICustomerRepository customerRepository, StripeService stripeService)
    {
      _mapper = mapper;
      _customerRepository = customerRepository;
      _stripeService = stripeService;
    }
    public async Task<CreateCustomerCommandResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
      var result = await _customerRepository.GetByEmail(command.Email);
      if (result != null)
      {
        throw new InvalidOperationException("Customer with same email already exists !");
      }
      else
      {
        var stripeCustomer = await _stripeService.GetByEmail(command.Email);

        //if (stripeCustomer == null)
        //{
        //  stripeCustomer = await _stripeService.Create(command);
        //}


        //We don't get null object, "stripeCustomer.Created" always filled 
        if (stripeCustomer.Email == null)
        {
          stripeCustomer = await _stripeService.Create(command);
        }

        var customer = _mapper.Map<Domain.DataModels.Customer>(command);

        customer.Id = Guid.NewGuid();
        customer.StripeCustomerId = stripeCustomer.Id;
        //customer.StripeCustomerName = stripeCustomer.Name;
        customer.StripeCustomerName = command.FirstName + " " + command.LastName;

        var response = await _customerRepository.Create(customer);

        if (response)
        {
          return new CreateCustomerCommandResult()
          {
            Payload = _mapper.Map<CustomerDto>(customer)
          };
        }
        else
        {
          return new CreateCustomerCommandResult()
          {
            Payload = null
          };
        }
      }


    }
  }
}


////Domain.DataModels.Customer customer= null;


//var customersList = _mapper.Map<List<CustomerDto>>(await _customerRepository.GetAll());

////if (stripeCustomersList.Any(x => x.Email.Equals(command.Email)) && customersList.Any(x => x.Email.Equals(command.Email)))
////{
////  throw new InvalidOperationException("Customer with same email already exists !");
////}

//if (stripeCustomersList.Any(x => x.Email.Equals(command.Email)))
//{
//  var temp = stripeCustomersList.FirstOrDefault(x => x.Email.Equals(command.Email));
//  //stripeCustomer.Id = temp.Id;

//  else
//  {

//    if (response)
//    {
//      return new CreateCustomerCommandResult()
//      {
//        Payload = _mapper.Map<CustomerDto>(customer)
//      };
//    }
//    else
//    {
//      return new CreateCustomerCommandResult()
//      {
//        Payload = null
//      };
//    }
//  }
//}

//else
//{


//  customer.StripeCustomerId = stripeCustomer.Id;
//  customer.StripeCustomerName = stripeCustomer.Name;

//  var response = await _customerRepository.Create(customer);
//  if (response)
//  {
//    return new CreateCustomerCommandResult()
//    {
//      Payload = _mapper.Map<CustomerDto>(customer)
//    };
//  }
//  else
//  {
//    return new CreateCustomerCommandResult()
//    {
//      Payload = null
//    };
//  }
//}

//return new CreateCustomerCommandResult()
//{
//  Id = stripeCustomer.Id,
//  Email = stripeCustomer.Email,
//  FullName = stripeCustomer.Name
//};


//CustomerId = customer.Id;
//return Ok(new CreateCustomerCommandResult() { CustomerId = customer.Id });
//throw new NotImplementedException();

