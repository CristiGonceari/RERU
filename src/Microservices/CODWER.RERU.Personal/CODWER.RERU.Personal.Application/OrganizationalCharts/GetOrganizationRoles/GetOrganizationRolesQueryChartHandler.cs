using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationRoles
{
    public class GetOrganizationRolesQueryChartHandler : IRequestHandler<GetOrganizationRolesChartQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetOrganizationRolesQueryChartHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetOrganizationRolesChartQuery request, CancellationToken cancellationToken)
        {
            var allRoles = _appDbContext.OrganizationRoles.AsQueryable();

            var organizationalChartRelations = _appDbContext.DepartmentRoleRelations
                .Where(x => x.OrganizationalChartId == request.OrganizationalChartId)
                .Where(x => x is ParentOrganizationRoleChildOrganizationRole || x is ParentOrganizationRoleChildDepartment || x is ParentDepartmentChildOrganizationRole);

            var organizationalChartRoles = new List<int>();

            foreach (var relation in organizationalChartRelations)
            {
                if (relation is ParentOrganizationRoleChildOrganizationRole oo)
                {
                    var parentRoleId = oo.ParentOrganizationRoleId;
                    if (parentRoleId != null)
                        organizationalChartRoles.Add(parentRoleId.Value);

                    var childRoleId = oo.ChildOrganizationRoleId;
                    organizationalChartRoles.Add(childRoleId);
                }else if (relation is ParentOrganizationRoleChildDepartment od)
                {
                    var parentRoleId = od.ParentOrganizationRoleId;
                    if (parentRoleId != null)
                        organizationalChartRoles.Add(parentRoleId.Value);
                }
                else if (relation is ParentDepartmentChildOrganizationRole departmentRole)
                {
                    var childRoleId = departmentRole.ChildOrganizationRoleId;
                    organizationalChartRoles.Add(childRoleId);
                }
            }

            organizationalChartRoles = organizationalChartRoles.Distinct().ToList();

            allRoles = allRoles.Where(r => !organizationalChartRoles.Contains(r.Id));

            return _mapper.Map<List<SelectItem>>(allRoles);
        }
    }

}
