using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentCalculated;
using CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentTemplate;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentDashboard
{
    public class GetDepartmentDashboardQueryHandler : IRequestHandler<GetDepartmentDashboardQuery, DepartmentDashboardDto>
    {
        private readonly IMediator _mediator;

        public GetDepartmentDashboardQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<DepartmentDashboardDto> Handle(GetDepartmentDashboardQuery request, CancellationToken cancellationToken)
        {
            var summary = new DepartmentRoleContentDto();

            var template = await _mediator.Send(new GetDepartmentRoleContentTemplateQuery {DepartmentId = request.DepartmentId});
            var actual = await _mediator.Send(new GetDepartmentRoleContentCalculatedQuery { Id = request.DepartmentId});

            foreach (var templateElement in template.Roles)
            {
                var newItem = new RoleFromDepartment
                {
                    OrganizationRoleId = templateElement.OrganizationRoleId,
                    OrganizationRoleName = templateElement.OrganizationRoleName
                };

                if (actual.Roles.Any(x => x.OrganizationRoleId == templateElement.OrganizationRoleId))
                {
                    var actualElement = actual.Roles.First(x => x.OrganizationRoleId == templateElement.OrganizationRoleId);

                    newItem.OrganizationRoleCount =  actualElement.OrganizationRoleCount - templateElement.OrganizationRoleCount;
                    newItem.ContractorIds = actualElement.ContractorIds;
                }
                else
                {
                    newItem.OrganizationRoleCount = - templateElement.OrganizationRoleCount;
                }

                summary.Roles.Add(newItem);
            }

            foreach (var actualElement in actual.Roles)
            {
                if (template.Roles.Any(x => x.OrganizationRoleId == actualElement.OrganizationRoleId)) continue;
                
                var newItem = new RoleFromDepartment
                {
                    OrganizationRoleId = actualElement.OrganizationRoleId,
                    OrganizationRoleName = actualElement.OrganizationRoleName,
                    OrganizationRoleCount = actualElement.OrganizationRoleCount,
                    ContractorIds = actualElement.ContractorIds
                };

                summary.Roles.Add(newItem);
            }

            return new DepartmentDashboardDto
            {
                Template = template,
                Actual = actual,
                Summary = summary
            };
        }
    }
}
