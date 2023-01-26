using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.OrganigramService.Enums;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.Organigrams.GetOrganigramUsers
{
    public class GetOrganigramUsersQuery : IRequest<List<UserProfileDto>>
    {
        public int Id { get; set; }
        public OrganizationalChartItemType Type { get; set; }
    }
}
