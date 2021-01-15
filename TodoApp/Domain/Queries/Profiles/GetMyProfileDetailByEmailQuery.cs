using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Queries.MyProfile
{
  public class GetMyProfileDetailByEmailQuery : IRequest<GetMyProfileDetailByEmailQueryResult>
  {
    public string Email { get; set; }
  }
}
