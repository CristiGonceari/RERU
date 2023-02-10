using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;

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
                    || (x is ParentDepartmentChildRole) && ((ParentDepartmentChildRole)x).ParentDepartmentId == null));

            if (abstractHead is ParentDepartmentChildDepartment)
            {
                var item = await _appDbContext.ParentDepartmentChildDepartments
                    .Include(x => x.ChildDepartment)
                    .FirstOrDefaultAsync(x => x.Id == abstractHead.Id);

                result.Id = item.ChildDepartment.Id;
                result.Name = item.ChildDepartment.Name;
                result.RelationId = item.Id;
                result.Type = OrganizationalChartItemType.Department;
                result.IsHead = true;
            }
            else if (abstractHead is ParentDepartmentChildRole)
            {
                var item = await _appDbContext.ParentDepartmentChildRoles
                    .Include(x => x.ChildRole)
                    .FirstOrDefaultAsync(x => x.Id == abstractHead.Id);

                result.Id = item.ChildRole.Id;
                result.Name = item.ChildRole.Name;
                result.RelationId = item.Id;
                result.Type = OrganizationalChartItemType.Role;
                result.IsHead = true;
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

                    var departmentRoles = _appDbContext.ParentDepartmentChildRoles
                        .Include(x => x.ChildRole)
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
                            Id = dr.ChildRole.Id,
                            Name = dr.ChildRole.Name,
                            Type = OrganizationalChartItemType.Role,
                            RelationId = dr.Id,
                            Childs = await GetChildrens(dr.ChildRole.Id, OrganizationalChartItemType.Role, organizationalChartId)
                        });
                    }

                    break;
                }

                case OrganizationalChartItemType.Role:
                {
                    var roleDepartments = _appDbContext.ParentRoleChildDepartments
                        .Include(x => x.ChildDepartment)
                        .Where(x => x.OrganizationalChartId == organizationalChartId && x.ParentRoleId == parentId);

                    var roleRoles = _appDbContext.ParentRoleChildRoles
                        .Include(x => x.ChildRole)
                        .Where(x => x.OrganizationalChartId == organizationalChartId && x.ParentRoleId == parentId);

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
                            Id = rr.ChildRole.Id,
                            Name = rr.ChildRole.Name,
                            Type = OrganizationalChartItemType.Role,
                            RelationId = rr.Id,
                            Childs = await GetChildrens(rr.ChildRole.Id, OrganizationalChartItemType.Role, organizationalChartId)
                        });
                    }

                    break;
                }
            }

            return listResult;
        }
    }
}
