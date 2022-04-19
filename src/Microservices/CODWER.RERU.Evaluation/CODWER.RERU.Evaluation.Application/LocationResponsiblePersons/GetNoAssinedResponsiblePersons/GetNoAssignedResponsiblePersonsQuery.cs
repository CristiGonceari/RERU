using CVU.ERP.Module.Application.Attributes;
using MediatR;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.GetNoAssinedResponsiblePersons
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_LOCAȚII)]
    public class GetNoAssignedResponsiblePersonsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public int LocationId { get; set; }
    }
}
