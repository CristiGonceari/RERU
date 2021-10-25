using CVU.ERP.Module.Application.Models;
using MediatR;

namespace CODWER.RERU.Core.Application.ApplicationUsers.Internal.GetInternalApplicationUser
{
    public class GetInternalApplicationUserQuery : IRequest<ApplicationUser>
    {
        public GetInternalApplicationUserQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}