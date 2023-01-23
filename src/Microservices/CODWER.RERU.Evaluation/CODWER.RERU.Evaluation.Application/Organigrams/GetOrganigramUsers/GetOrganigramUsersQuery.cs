using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.OrganigramService.Enums;
using CVU.ERP.OrganigramService.Models;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Organigrams.GetOrganigramUsers
{
    public class GetOrganigramUsersQuery : IRequest<List<UserProfileDto>>
    {
        public int Id { get; set; }
        public OrganizationalChartItemType Type { get; set; }
    }
}
