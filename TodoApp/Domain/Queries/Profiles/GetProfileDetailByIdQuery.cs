using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Queries.Profiles
{
    public class GetProfileDetailByIdQuery : IRequest<GetProfileDetailByIdQueryResult>
    {
        public Guid Id { get; set; }
    }
}
