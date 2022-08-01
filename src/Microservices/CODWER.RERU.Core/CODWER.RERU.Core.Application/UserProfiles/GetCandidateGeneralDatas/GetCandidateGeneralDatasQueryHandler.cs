using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.GetCandidateGeneralDatas
{
    public class GetCandidateGeneralDatasQueryHandler : BaseHandler, IRequestHandler<GetCandidateGeneralDatasQuery, EditCandidateDto>
    {
        public GetCandidateGeneralDatasQueryHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider) { }

        public async Task<EditCandidateDto> Handle(GetCandidateGeneralDatasQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await AppDbContext.UserProfiles
                .Include(up => up.Contractor)
                .FirstOrDefaultAsync(u => u.Id == request.Id);

            var userProfDto = Mapper.Map<EditCandidateDto>(userProfile);

            return userProfDto;
        }
    }
}
