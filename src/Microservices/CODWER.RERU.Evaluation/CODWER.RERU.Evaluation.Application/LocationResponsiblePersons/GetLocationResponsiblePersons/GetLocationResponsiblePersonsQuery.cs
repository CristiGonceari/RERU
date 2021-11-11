using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.GetLocationResponsiblePersons
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATION_RESPONSIBLE_PERSONS_GENERAL_ACCESS)]
    public class GetLocationResponsiblePersonsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public int LocationId { get; set; }
    }
}
