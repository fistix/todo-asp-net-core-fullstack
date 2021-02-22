using Fistix.Training.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core
{
  public interface IStripeRepository
  {
    Task<bool> Create(Customer customer);

    Task<Customer> GetByEmail(string email);

    Task<List<Customer>> GetAll();

  }
}
