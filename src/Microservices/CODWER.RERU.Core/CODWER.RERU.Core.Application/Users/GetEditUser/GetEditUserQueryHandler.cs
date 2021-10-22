using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.GetEditUser
{
    public class GetEditUserQueryHandler : BaseHandler, IRequestHandler<GetEditUserQuery, UserDto>
    {
        public GetEditUserQueryHandler(ICommonServiceProvider commonServicepProvider) : base(commonServicepProvider)
        {
        }

        public async Task<UserDto> Handle(GetEditUserQuery request, CancellationToken cancellationToken)
        {
              var user = await UserManagementDbContext.Users
                .Where (d => d.Id == request.Id)
                .FirstOrDefaultAsync ();
                
            return Mapper.Map<UserDto> (user);
        }
    }
}