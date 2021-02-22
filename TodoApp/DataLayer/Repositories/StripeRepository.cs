using Fistix.Training.Core;
using Fistix.Training.Core.Exceptions;
using Fistix.Training.Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.DataLayer.Repositories
{
  public class StripeRepository : IStripeRepository
  {
    private readonly EfContext _efContext = null;
    public StripeRepository(EfContext efContext)
    {
      _efContext = efContext;
    }


    public async Task<bool> Create(Customer customer)
    {
      if (_efContext.Customers.Any(x => x.Email.Equals(customer.Email)))
      {
        throw new InvalidOperationException("Customer with same email already exist!");
      }
      await _efContext.Customers.AddAsync(customer);
      var dbChangesCount = await _efContext.SaveChangesAsync();
      return dbChangesCount > 0;
    }


    public async Task<Customer> GetByEmail(string email)
    {
      var customer = await _efContext.Customers.FirstOrDefaultAsync(x => x.Email.Equals(email));
      if (customer == null)
      {
        throw new NotFoundException();
        //throw new NotFoundException("Result not found!");
      }
      return customer;
    }

    public async Task<List<Customer>> GetAll()
    {
      return await _efContext.Customers.ToListAsync();
    }

  }
}
