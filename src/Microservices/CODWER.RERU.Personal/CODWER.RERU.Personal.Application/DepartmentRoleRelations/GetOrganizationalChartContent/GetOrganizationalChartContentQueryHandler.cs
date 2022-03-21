using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.GetOrganizationalChartContent
{
    public class GetOrganizationalChartContentQueryHandler : IRequestHandler<GetOrganizationalChartContentQuery, OrganizationalChartContentDto>
    {
        private readonly AppDbContext _appDbContext;

        public GetOrganizationalChartContentQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OrganizationalChartContentDto> Handle(GetOrganizationalChartContentQuery request, CancellationToken cancellationToken)
        {
            var head = await GetHead(request.OrganizationalChartId);
            
            head.Childs = await GetChildrens(head.Id, head.Type, request.OrganizationalChartId);

            return head;
        }

        private async Task<OrganizationalChartContentDto> GetHead(int organizationalChartId)
        {
            var result = new OrganizationalChartContentDto();

            var abstractHead = await _appDbContext.DepartmentRoleRelations.FirstOrDefaultAsync(x =>
                x.OrganizationalChartId == organizationalChartId
                && ((x is ParentDepartmentChildDepartment) && ((ParentDepartmentChildDepartment)x).ParentDepartmentId == null
                    || (x is ParentDepartmentChildOrganizationRole) && ((ParentDepartmentChildOrganizationRole)x).ParentDepartmentId == null));

            if (abstractHead is ParentDepartmentChildDepartment)
            {
                var item = await _appDbContext.ParentDepartmentChildDepartments
                    .Include(x => x.ChildDepartment)
                    .FirstOrDefaultAsync(x => x.Id == abstractHead.Id);

                result.Id = item.ChildDepartment.Id;
                result.Name = item.ChildDepartment.Name;
                result.RelationId = item.Id;
                result.Type = OrganizationalChartItemType.Department;
            }
            else if (abstractHead is ParentDepartmentChildOrganizationRole)
            {
                var item = await _appDbContext.ParentDepartmentChildOrganizationRoles
                    .Include(x => x.ChildOrganizationRole)
                    .FirstOrDefaultAsync(x => x.Id == abstractHead.Id);

                result.Id = item.ChildOrganizationRole.Id;
                result.Name = item.ChildOrganizationRole.Name;
                result.RelationId = item.Id;
                result.Type = OrganizationalChartItemType.OrganizationRole;
            }

            return result;
        }

        private async Task<List<OrganizationalChartContentDto>> GetChildrens(int parentId, OrganizationalChartItemType type, int organizationalChartId)
        {
            var listResult = new List<OrganizationalChartContentDto>();

            switch (type)
            {
                case OrganizationalChartItemType.Department:
                {
                    var departmentDepartments = _appDbContext.ParentDepartmentChildDepartments
                        .Include(x => x.ChildDepartment)
                        .Where(x => x.OrganizationalChartId == organizationalChartId && x.ParentDepartmentId == parentId);

                    var departmentRoles = _appDbContext.ParentDepartmentChildOrganizationRoles
                        .Include(x => x.ChildOrganizationRole)
                        .Where(x => x.OrganizationalChartId == organizationalChartId && x.ParentDepartmentId == parentId);

                    foreach (var dd in departmentDepartments.ToList())
                    {
                        listResult.Add(new OrganizationalChartContentDto
                        {
                            Id = dd.ChildDepartment.Id,
                            Name = dd.ChildDepartment.Name,
                            Type = OrganizationalChartItemType.Department,
                            RelationId = dd.Id,
                            Childs = await GetChildrens(dd.ChildDepartment.Id, OrganizationalChartItemType.Department, organizationalChartId)
                        });
                    }

                    foreach (var dr in departmentRoles.ToList())
                    {
                        listResult.Add(new OrganizationalChartContentDto
                        {
                            Id = dr.ChildOrganizationRole.Id,
                            Name = dr.ChildOrganizationRole.Name,
                            Type = OrganizationalChartItemType.OrganizationRole,
                            RelationId = dr.Id,
                            Childs = await GetChildrens(dr.ChildOrganizationRole.Id, OrganizationalChartItemType.OrganizationRole, organizationalChartId)
                        });
                    }

                    break;
                }

                case OrganizationalChartItemType.OrganizationRole:
                {
                    var roleDepartments = _appDbContext.ParentOrganizationRoleChildDepartments
                        .Include(x => x.ChildDepartment)
                        .Where(x => x.OrganizationalChartId == organizationalChartId && x.ParentOrganizationRoleId == parentId);

                    var roleRoles = _appDbContext.ParentOrganizationRoleChildOrganizationRoles
                        .Include(x => x.ChildOrganizationRole)
                        .Where(x => x.OrganizationalChartId == organizationalChartId && x.ParentOrganizationRoleId == parentId);

                    foreach (var rd in roleDepartments.ToList())
                    {
                        listResult.Add(new OrganizationalChartContentDto
                        {
                            Id = rd.ChildDepartment.Id,
                            Name = rd.ChildDepartment.Name,
                            Type = OrganizationalChartItemType.Department,
                            RelationId = rd.Id,
                            Childs = await GetChildrens(rd.ChildDepartment.Id, OrganizationalChartItemType.Department, organizationalChartId)
                        });
                    }

                    foreach (var rr in roleRoles.ToList())
                    {
                        listResult.Add(new OrganizationalChartContentDto
                        {
                            Id = rr.ChildOrganizationRole.Id,
                            Name = rr.ChildOrganizationRole.Name,
                            Type = OrganizationalChartItemType.OrganizationRole,
                            RelationId = rr.Id,
                            Childs = await GetChildrens(rr.ChildOrganizationRole.Id, OrganizationalChartItemType.OrganizationRole, organizationalChartId)
                        });
                    }

                    break;
                }
            }

            return listResult;
        }
    }
}
