using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.RemoveDepartmentRoleRelation
{
    public class RemoveDepartmentRoleRelationHandler : IRequestHandler<RemoveDepartmentRoleRelationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private List<DepartmentRoleRelation> _listToRemove;

        public RemoveDepartmentRoleRelationHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _listToRemove = new List<DepartmentRoleRelation>();
        }

        public async Task<Unit> Handle(RemoveDepartmentRoleRelationCommand request, CancellationToken cancellationToken)
        {
            var head = await _appDbContext.DepartmentRoleRelations.FirstAsync(x => x.Id == request.Id);

            var headDetails = await GetFirstItem(head);
            await GetAllSubItems(headDetails.Id, headDetails.Type, head.OrganizationalChartId);

            _appDbContext.DepartmentRoleRelations.RemoveRange(_listToRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task<OrganizationalChartContentDto> GetFirstItem(DepartmentRoleRelation head)
        {
            var result = new OrganizationalChartContentDto();
            _listToRemove.Add(head);

            if (head is ParentDepartmentChildDepartment)
            {
                var item = await _appDbContext.ParentDepartmentChildDepartments
                    .Include(x => x.ChildDepartment)
                    .FirstOrDefaultAsync(x => x.Id == head.Id);

                result.Id = item.ChildDepartment.Id;
                result.RelationId = item.Id;
                result.Type = OrganizationalChartItemType.Department;
            }
            else if (head is ParentRoleChildDepartment)
            {
                var item = await _appDbContext.ParentRoleChildDepartments
                    .Include(x => x.ChildDepartment)
                    .FirstOrDefaultAsync(x => x.Id == head.Id);

                result.Id = item.ChildDepartment.Id;
                result.RelationId = item.Id;
                result.Type = OrganizationalChartItemType.Department;
            }
            else if (head is ParentDepartmentChildRole)
            {
                var item = await _appDbContext.ParentDepartmentChildRoles
                    .Include(x => x.ChildRole)
                    .FirstOrDefaultAsync(x => x.Id == head.Id);

                result.Id = item.ChildRole.Id;
                result.RelationId = item.Id;
                result.Type = OrganizationalChartItemType.Role;
            }
            else if (head is ParentRoleChildRole)
            {
                var item = await _appDbContext.ParentRoleChildRoles
                    .Include(x => x.ChildRole)
                    .FirstOrDefaultAsync(x => x.Id == head.Id);

                result.Id = item.ChildRole.Id;
                result.RelationId = item.Id;
                result.Type = OrganizationalChartItemType.Role;
            }

            return result;
        }

        private async Task GetAllSubItems(int parentId, OrganizationalChartItemType type, int organizationalChartId)
        {
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

                        foreach (var dd in departmentDepartments)
                        {
                            _listToRemove.Add(dd);
                            await GetAllSubItems(dd.ChildDepartmentId, OrganizationalChartItemType.Department, organizationalChartId);
                        }

                        foreach (var dr in departmentRoles)
                        {
                            _listToRemove.Add(dr);
                            await GetAllSubItems(dr.ChildRole.Id, OrganizationalChartItemType.Role, organizationalChartId);
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

                        foreach (var rd in roleDepartments)
                        {
                            _listToRemove.Add(rd);
                            await GetAllSubItems(rd.ChildDepartment.Id, OrganizationalChartItemType.Department, organizationalChartId);
                        }

                        foreach (var rr in roleRoles)
                        {
                            _listToRemove.Add(rr);
                            await GetAllSubItems(rr.ChildRole.Id, OrganizationalChartItemType.Role, organizationalChartId);
                        }

                        break;
                    }
            }
        }

        //private async Task GetAllSubItems(DepartmentRoleRelation departmentRoleRelation)
        //{
        //    _listToRemove.Add(departmentRoleRelation);

        //    var childId = await GetChildId(departmentRoleRelation);

        //    var itemsToRemove = _appDbContext.DepartmentRoleRelations
        //        .Where(x => 
        //                       ((x is ParentDepartmentChildDepartment) && ((ParentDepartmentChildDepartment) x).ParentDepartmentId == childId)
        //                    || ((x is ParentDepartmentChildRole) && ((ParentDepartmentChildRole) x).ParentDepartmentId == childId)
        //                    || ((x is ParentRoleChildDepartment) && ((ParentRoleChildDepartment) x).ParentRoleId == childId)
        //                    || ((x is ParentRoleChildRole) && ((ParentRoleChildRole) x).ParentRoleId == childId))
        //        .ToList();

        //    foreach (var item in itemsToRemove)
        //    {
        //        await GetAllSubItems(item);
        //    }
        //}

        //private async Task<int> GetChildId(DepartmentRoleRelation departmentRoleRelation)
        //{
        //    if (departmentRoleRelation is ParentDepartmentChildDepartment department)
        //    {
        //        return department.ChildDepartmentId;
        //    }
        //    if (departmentRoleRelation is ParentDepartmentChildRole department2)
        //    {
        //        return department2.ChildRoleId;
        //    }
        //    if (departmentRoleRelation is ParentRoleChildDepartment orgRole1)
        //    {
        //        return orgRole1.ChildDepartmentId;
        //    }
        //    if (departmentRoleRelation is ParentRoleChildRole orgRole2)
        //    {
        //        return orgRole2.ChildRoleId;
        //    }

        //    return 0;
        //}
    }
}