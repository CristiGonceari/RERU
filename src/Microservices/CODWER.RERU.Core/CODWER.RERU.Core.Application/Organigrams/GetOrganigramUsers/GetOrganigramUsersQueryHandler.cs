using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.OrganigramService.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Organigrams.GetOrganigramUsers
{
    public class GetOrganigramUsersQueryHandler : IRequestHandler<GetOrganigramUsersQuery, List<UserProfileDto>>
    {
        private readonly IGetOrganigramService _service;
        private readonly IMapper _mapper;

        public GetOrganigramUsersQueryHandler(IGetOrganigramService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<List<UserProfileDto>> Handle(GetOrganigramUsersQuery request, CancellationToken cancellationToken)
        {
            var userProfiles = await _service.GetOrganigramUserProfiles(request.Id, request.Type);

            var mapped = _mapper.Map<List<UserProfileDto>>(userProfiles);

            return mapped;
        }
    }
}
