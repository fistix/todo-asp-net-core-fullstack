using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Stripe;
using Fistix.Training.Domain.Dtos;
using MediatR;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.Stripe
{
  public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResult>
  {

    private readonly IMapper _mapper = null;
    private readonly IStripeRepository _stripeRepository = null;
    public CreateCustomerCommandHandler(IMapper mapper, IStripeRepository stripeRepository)
    {
      _mapper = mapper;
      _stripeRepository = stripeRepository;
    }
    public async Task<CreateCustomerCommandResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
      var customer = _mapper.Map<Domain.DataModels.Customer>(command);
      customer.Id = Guid.NewGuid();
      //Domain.DataModels.Customer customer= null;

      Customer stripeCustomer = new Customer();
      #region GetCustomersList
      var CustomerOptions = new CustomerListOptions
      {
        Limit = 50,
        Email = command.Email
      };
      var customerService = new CustomerService();
      StripeList<Customer> stripeCustomersList = customerService.List(
        CustomerOptions
        );
      #endregion
      var customersList = _mapper.Map<List<CustomerDto>>(await _stripeRepository.GetAll());

      //if (stripeCustomersList.Any(x => x.Email.Equals(command.Email)) && customersList.Any(x => x.Email.Equals(command.Email)))
      //{
      //  throw new InvalidOperationException("Customer with same email already exists !");
      //}

      if (stripeCustomersList.Any(x => x.Email.Equals(command.Email)))
      {
        var temp = stripeCustomersList.FirstOrDefault(x => x.Email.Equals(command.Email));
        //stripeCustomer.Id = temp.Id;

        if (customersList.Any(x => x.Email.Equals(command.Email)))
        {
          throw new InvalidOperationException("Customer with same email already exists !");
        }

        else
        {
          customer.StripeCustomerId = temp.Id;
          customer.StripeCustomerName = temp.Name;

          var response = await _stripeRepository.Create(customer);
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

      else
      {
        stripeCustomer = customerService.Create(new CustomerCreateOptions
        {
          Email = command.Email,
          Name = command.FirstName + " " + command.LastName
        });

        customer.StripeCustomerId = stripeCustomer.Id;
        customer.StripeCustomerName = stripeCustomer.Name;

        var response = await _stripeRepository.Create(customer);
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

      //return new CreateCustomerCommandResult()
      //{
      //  Id = stripeCustomer.Id,
      //  Email = stripeCustomer.Email,
      //  FullName = stripeCustomer.Name
      //};
      
      
      //CustomerId = customer.Id;
      //return Ok(new CreateCustomerCommandResult() { CustomerId = customer.Id });
      //throw new NotImplementedException();
    }
  }
}
