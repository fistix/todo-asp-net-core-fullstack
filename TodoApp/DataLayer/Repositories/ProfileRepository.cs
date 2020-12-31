using Fistix.Training.Core;
using Fistix.Training.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.DataLayer.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly EfContext _efContext = null;
        public ProfileRepository(EfContext efContext)
        {
            _efContext = efContext;
        }


        public async Task<Profile> Create(Profile profile)
        {
            var entity = await _efContext.Profiles.AddAsync(profile);
            await _efContext.SaveChangesAsync();
            return entity.Entity;
        }
    }
}
