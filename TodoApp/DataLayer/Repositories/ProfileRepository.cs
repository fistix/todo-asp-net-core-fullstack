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
    public class ProfileRepository : IProfileRepository
    {
        private readonly EfContext _efContext = null;
        public ProfileRepository(EfContext efContext)
        {
            _efContext = efContext;
        }


        public async Task<Profile> Create(Profile profile)
        {
            if (_efContext.Profiles.Any(x => x.Email == profile.Email))
            {
                throw new ArgumentException("Email is already registered!");
            }
            var entity = await _efContext.Profiles.AddAsync(profile);
            await _efContext.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Profile> Update(Profile profile)
        {
            var temp =  _efContext.Profiles.FirstOrDefault(x=> x.Id.Equals( profile.Id));
            if (temp != null)
            {
                temp.FirstName = profile.FirstName;
                temp.LastName = profile.LastName;
                var entity = _efContext.Profiles.Update(temp);
                await _efContext.SaveChangesAsync();
                return entity.Entity;
            }
            throw new NotFoundException("Result not found!");
        }

        public async Task<List<Profile>> GetAll()
        {
            var entity = await _efContext.Profiles.ToListAsync();
            return entity;
        }
        public async Task<Profile> GetByEmail(string email)
        {
            var profile = await _efContext.Profiles.FirstOrDefaultAsync(x => x.Email.Equals(email));
            if (profile == null)
            {
                throw new NotFoundException("Email does not exists!");
                //throw new NotFoundException("Result not found!");
            }
            return profile;
        }

        public async Task<Profile> GetById(Guid id)
        {
            var profile = await _efContext.Profiles.FindAsync(id);
            if (profile == null)
            {
                throw new NotFoundException("Result not found!");
            }
            return profile;
        }
    }
}
