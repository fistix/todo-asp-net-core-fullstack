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
  public class CustomerRepository : ICustomerRepository
  {
    private readonly EfContext _efContext = null;
    public CustomerRepository(EfContext efContext)
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

    public async Task<bool> Update(Customer customer)
    {
      _efContext.Customers.Update(customer);
      var dbChangesCount = await _efContext.SaveChangesAsync();
      return dbChangesCount > 0;
    }

    public async Task<List<Customer>> GetAll()
    {
      return await _efContext.Customers.ToListAsync();
    }

    public async Task<Customer> GetById(Guid id)
    {
      //var profile = await _efContext.Profiles.FindAsync(id);
      var customer = await _efContext.Customers.FirstOrDefaultAsync(x => x.Id.Equals(id));
      if (customer == null)
      {
        //return null;
        throw new NotFoundException("Customer not found!");
      }
      return customer;
    }

    public async Task<Customer> GetByEmail(string email)
    {
      return await _efContext.Customers.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }
  }
}
