using CVU.ERP.Module.Application.Models;
using MediatR;

namespace CODWER.RERU.Core.Application.ApplicationUsers.Internal.GetInternalApplicationUserByIdentity
{
    public class GetInternalApplicationUserByIdentityQuery : IRequest<ApplicationUser>
    {
        public GetInternalApplicationUserByIdentityQuery(string id, string identityProvider)
        {
            IdentityType = identityProvider;
            Identity = id;
        }

        public string IdentityType { set; get; }
        public string Identity { set; get; }
    }
}