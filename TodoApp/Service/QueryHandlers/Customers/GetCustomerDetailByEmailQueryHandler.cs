using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.Customers;
using MediatR;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.QueryHandlers.Stripe
{
  public class GetCustomerDetailByEmailQueryHandler : IRequestHandler<GetCustomerDetailByEmailQuery, GetCustomerDetailByEmailQueryResult>
  {
    private readonly IMapper _mapper = null;
    private readonly ICustomerRepository _customerRepository = null;


    public GetCustomerDetailByEmailQueryHandler(IMapper mapper, ICustomerRepository customerRepository)
    {
      _mapper = mapper;
      _customerRepository = customerRepository;
    }
    public async Task<GetCustomerDetailByEmailQueryResult> Handle(GetCustomerDetailByEmailQuery query, CancellationToken cancellationToken)
    {
      var result = _mapper.Map<CustomerDto>(await _customerRepository.GetByEmail(query.Email));
      return new GetCustomerDetailByEmailQueryResult()
      {
        Payload = result
      };

      //Customer stripeCustomer = new Customer();
      //#region GetCustomersList
      //var CustomerOptions = new CustomerListOptions
      //{
      //  Limit = 50,
      //  Email = request.Email
      //};
      //var customerService = new CustomerService();
      //StripeList<Customer> customersList = customerService.List(
      //  CustomerOptions
      //  );
      //#endregion

      //if (customersList.Any(x => x.Email.Equals(request.Email)))
      //{
      //  var temp = customersList.FirstOrDefault(x => x.Email.Equals(request.Email));
      //  stripeCustomer.Id = temp.Id;

      //  return new GetCustomerDetailByEmailQueryResult()
      //  {
      //    CustomerId = stripeCustomer.Id
      //  };

      //}
     
      //return new GetCustomerDetailByEmailQueryResult()
      //{
      //  CustomerId = null
      //};
      
      
    }
  }
}
