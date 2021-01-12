using Fistix.Training.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core
{
    public interface IProfileRepository
    {
        Task<bool> Create(Profile profile);
        Task<bool> Update(Profile profile);
        Task<bool> Delete(Guid id);
        Task<Profile> GetById(Guid id);
        Task<Profile> GetByEmail(string email);
        Task<List<Profile>> GetAll();
    }
}
