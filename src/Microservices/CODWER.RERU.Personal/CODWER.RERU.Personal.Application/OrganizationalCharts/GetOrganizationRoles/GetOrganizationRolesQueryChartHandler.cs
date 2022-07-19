using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationRoles
{
    public class GetRolesQueryChartHandler : IRequestHandler<GetRolesChartQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetRolesQueryChartHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetRolesChartQuery request, CancellationToken cancellationToken)
        {
            var allRoles = _appDbContext.Roles.AsQueryable();

            var organizationalChartRelations = _appDbContext.DepartmentRoleRelations
                .Where(x => x.OrganizationalChartId == request.OrganizationalChartId)
                .Where(x => x is ParentRoleChildRole || x is ParentRoleChildDepartment || x is ParentDepartmentChildRole);

            var organizationalChartRoles = new List<int>();

            foreach (var relation in organizationalChartRelations)
            {
                if (relation is ParentRoleChildRole oo)
                {
                    var parentRoleId = oo.ParentRoleId;
                    if (parentRoleId != null)
                        organizationalChartRoles.Add(parentRoleId.Value);

                    var childRoleId = oo.ChildRoleId;
                    organizationalChartRoles.Add(childRoleId);
                }else if (relation is ParentRoleChildDepartment od)
                {
                    var parentRoleId = od.ParentRoleId;
                    if (parentRoleId != null)
                        organizationalChartRoles.Add(parentRoleId.Value);
                }
                else if (relation is ParentDepartmentChildRole departmentRole)
                {
                    var childRoleId = departmentRole.ChildRoleId;
                    organizationalChartRoles.Add(childRoleId);
                }
            }

            organizationalChartRoles = organizationalChartRoles.Distinct().ToList();

            allRoles = allRoles.Where(r => !organizationalChartRoles.Contains(r.Id));

            return _mapper.Map<List<SelectItem>>(allRoles);
        }
    }

}
