using CVU.ERP.ServiceProvider.Models;
using MediatR;

namespace CODWER.RERU.Core.Application.ApplicationUsers.GetApplicationUser
{
    public class GetApplicationUserQuery : IRequest<ApplicationUser>
    {
        public GetApplicationUserQuery(string id, string identityProvider)
        {
            Id = id;
            IdentityProvider = identityProvider;
        }
        public GetApplicationUserQuery(int userProfileId)
        {
            UserProfileId = userProfileId;
        }

        public string Id { set; get; }
        public string IdentityProvider { set; get; }
        public int UserProfileId { set; get; }
    }
}