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


    public async Task<bool> Create(Profile profile)
    {
      if (_efContext.Profiles.Any(x => x.Email.Equals(profile.Email)))
        throw new InvalidOperationException("Profile with same email already exist!");
      

      await _efContext.Profiles.AddAsync(profile);
      var dbChangesCount = await _efContext.SaveChangesAsync();
      return dbChangesCount > 0;
    }

    public async Task<bool> Update(Profile profile)
    {
      _efContext.Profiles.Update(profile);
      var dbChangesCount = await _efContext.SaveChangesAsync();
      return dbChangesCount > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
      var profile = await GetById(id);
      if (profile != null)
      {
        _efContext.Profiles.Remove(profile);
        var dbChangesCount = await _efContext.SaveChangesAsync();
        return dbChangesCount > 0;
      }
      throw new NotFoundException();
    }

    public async Task<List<Profile>> GetAll()
    {
      return await _efContext.Profiles.ToListAsync();
    }

    public async Task<Profile> GetById(Guid id)
    {
      //var profile = await _efContext.Profiles.FindAsync(id);
      var profile = await _efContext.Profiles.FirstOrDefaultAsync(x => x.Id.Equals(id));
      if (profile == null)
      {
        throw new NotFoundException("Profile not found!");
      }
      return profile;
    }

    public async Task<Profile> GetByEmail(string email)
    {
      var profile = await _efContext.Profiles.FirstOrDefaultAsync(x => x.Email.Equals(email));
      if (profile == null)
      {
        throw new NotFoundException();
        //throw new NotFoundException("Result not found!");
      }
      return profile;
    }
  
  }
}