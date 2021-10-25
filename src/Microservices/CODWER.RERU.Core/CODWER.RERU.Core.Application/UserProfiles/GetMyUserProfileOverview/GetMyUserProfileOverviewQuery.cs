using CODWER.RERU.Core.DataTransferObjects.Profile;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.GetMyUserProfileOverview {
    //[ModuleOperation (requiresAuthentication: true)]
    public class GetMyUserProfileOverviewQuery : IRequest<UserProfileOverviewDto> {

    }
}