using Fistix.Training.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Queries.MyProfile
{
  public class GetMyProfileDetailByEmailQueryResult
  {
    public ProfileDto Payload { get; set; }
  }
}
