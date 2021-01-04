using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.QueryHandlers.Profiles
{
    public class GetProfileDetailByEmailQueryHandler : IRequestHandler<GetProfileDetailByEmailQuery, GetProfileDetailByEmailQueryResult>
    {
        private readonly IMapper _mapper = null;
        private readonly IProfileRepository _profileRepository = null;
        public GetProfileDetailByEmailQueryHandler(IMapper mapper,IProfileRepository profileRepository)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
        }

        public async Task<GetProfileDetailByEmailQueryResult> Handle(GetProfileDetailByEmailQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<ProfileDto>(await _profileRepository.GetByEmail(request.Email));
            //if (result == null)
            //{
            //    throw new ArgumentException("Email does not exists!");
            //}
            return new GetProfileDetailByEmailQueryResult()
            {
                Payload = result
            };
        }
    }
}
