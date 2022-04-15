using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.GetEditUserPersonalDetails 
{
    public class GetEditUserPersonalDetailsQueryHandler : BaseHandler, IRequestHandler<GetEditUserPersonalDetailsQuery, EditUserPersonalDetailsDto> 
    {
        public GetEditUserPersonalDetailsQueryHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }

        public async Task<EditUserPersonalDetailsDto> Handle (GetEditUserPersonalDetailsQuery request, CancellationToken cancellationToken) 
        {
            var userProfile = await AppDbContext.UserProfiles
                .FirstOrDefaultAsync (up => up.Id == request.Id);

            return Mapper.Map<EditUserPersonalDetailsDto> (userProfile);
        }
    }
}