using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.GetUserDetailsByEmail
{
    public class GetUserDetailsByEmailQueryHandler : BaseHandler, IRequestHandler<GetUserDetailsByEmailQuery, UserDetailsOverviewDto>
    {
        public GetUserDetailsByEmailQueryHandler(ICommonServiceProvider commonServicepProvider) : base(commonServicepProvider) { }

        public async Task<UserDetailsOverviewDto> Handle(GetUserDetailsByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await AppDbContext.UserProfiles
                .FirstOrDefaultAsync(d => d.Email == request.Email);

            var result = new UserDetailsOverviewDto() 
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FatherName = user.FatherName,
                    MediaFileId = user.MediaFileId,
                    PhoneNumber = user.PhoneNumber.Remove(4,5).Insert(4,"*****")
            };

            return result;
        }
    }
}
