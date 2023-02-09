using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentCalculated;
using CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentTemplate;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentDashboard
{
    public class GetDepartmentDashboardQueryHandler : IRequestHandler<GetDepartmentDashboardQuery, DepartmentDashboardDto>
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;

        public GetDepartmentDashboardQueryHandler(IMediator mediator, AppDbContext appDbContext)
        {
            _mediator = mediator;
            _appDbContext = appDbContext;
        }

        public async Task<DepartmentDashboardDto> Handle(GetDepartmentDashboardQuery request, CancellationToken cancellationToken)
        {
            var summary = new DepartmentRoleContentDto();

            var template = await _mediator.Send(new GetDepartmentRoleContentTemplateQuery {DepartmentId = request.DepartmentId});
            var actual = await GetDepartmentRoleContent(request.DepartmentId, 1);

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

        private async Task<DepartmentRoleContentDto> GetDepartmentRoleContent(int id, int type)
        {
            var positions = _appDbContext.UserProfiles
                .Include(p => p.Contractor)
                .Include(p => p.Department)
                .Include(p => p.Role)
                .Include(p => p.EmployeeFunction)
                .AsQueryable();

            if (type == 1)
            {
                positions = positions.Where(p => p.Department.Id == id);
            }
            else if (type == 2)
            {
                positions = positions.Where(p => p.Role.Id == id);

            }

            var departmentContent = positions
                .ToList()
                .Where(p => p.Role != null)
                .GroupBy(p => p.Department) // group by department
                .Select(drc => new DepartmentRoleContentDto
                {
                    DepartmentId = drc.Key.Id,
                    DepartmentName = drc.Key.Name,
                    Roles = drc
                        .GroupBy(p => p.Role) // group by organization role
                        .Select(rfd => new RoleFromDepartment
                        {
                            OrganizationRoleId = rfd.Key.Id,
                            OrganizationRoleName = rfd.Key.Name,

                            OrganizationRoleCount = rfd.ToList().Select(c => c.Id).Distinct().Count(),
                            ContractorIds = rfd.ToList().Select(c => new SelectItem
                            {
                                Label = c.FullName,
                                Value = c.Id.ToString()
                            }).Distinct().ToList()
                        }).ToList(),
                }).ToList();

            return departmentContent.Count() != 0
                ? departmentContent.First()
                : new DepartmentRoleContentDto();
        }
    }
}
