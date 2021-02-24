using Fistix.Training.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core
{
  public interface ICustomerRepository
  {
    Task<bool> Create(Customer customer);

    Task<bool> Update(Customer customer);

    Task<List<Customer>> GetAll();

    Task<Customer> GetById(Guid id);

    Task<Customer> GetByEmail(string email);

  }
}
