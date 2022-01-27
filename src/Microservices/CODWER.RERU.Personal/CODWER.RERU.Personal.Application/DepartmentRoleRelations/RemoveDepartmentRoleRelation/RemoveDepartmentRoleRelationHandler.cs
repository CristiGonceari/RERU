using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;
using Microsoft.EntityFrameworkCore;

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

            if (head is ParentDepartmentChildDepartment || head is ParentOrganizationRoleChildDepartment)
            {
                var item = await _appDbContext.ParentDepartmentChildDepartments
                    .Include(x => x.ChildDepartment)
                    .FirstOrDefaultAsync(x => x.Id == head.Id);

                result.Id = item.ChildDepartment.Id;
                result.RelationId = item.Id;
                result.Type = OrganizationalChartItemType.Department;
            }
            else if (head is ParentDepartmentChildOrganizationRole || head is ParentOrganizationRoleChildOrganizationRole)
            {
                var item = await _appDbContext.ParentDepartmentChildOrganizationRoles
                    .Include(x => x.ChildOrganizationRole)
                    .FirstOrDefaultAsync(x => x.Id == head.Id);

                result.Id = item.ChildOrganizationRole.Id;
                result.RelationId = item.Id;
                result.Type = OrganizationalChartItemType.OrganizationRole;
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

                        var departmentRoles = _appDbContext.ParentDepartmentChildOrganizationRoles
                            .Include(x => x.ChildOrganizationRole)
                            .Where(x => x.OrganizationalChartId == organizationalChartId && x.ParentDepartmentId == parentId);

                        foreach (var dd in departmentDepartments)
                        {
                            _listToRemove.Add(dd);
                            await GetAllSubItems(dd.ChildDepartmentId, OrganizationalChartItemType.Department, organizationalChartId);
                        }

                        foreach (var dr in departmentRoles)
                        {
                            _listToRemove.Add(dr);
                            await GetAllSubItems(dr.ChildOrganizationRole.Id, OrganizationalChartItemType.OrganizationRole, organizationalChartId);
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

                        foreach (var rd in roleDepartments)
                        {
                            _listToRemove.Add(rd);
                            await GetAllSubItems(rd.ChildDepartment.Id, OrganizationalChartItemType.Department, organizationalChartId);
                        }

                        foreach (var rr in roleRoles)
                        {
                            _listToRemove.Add(rr);
                            await GetAllSubItems(rr.ChildOrganizationRole.Id, OrganizationalChartItemType.OrganizationRole, organizationalChartId);
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
        //                    || ((x is ParentDepartmentChildOrganizationRole) && ((ParentDepartmentChildOrganizationRole) x).ParentDepartmentId == childId)
        //                    || ((x is ParentOrganizationRoleChildDepartment) && ((ParentOrganizationRoleChildDepartment) x).ParentOrganizationRoleId == childId)
        //                    || ((x is ParentOrganizationRoleChildOrganizationRole) && ((ParentOrganizationRoleChildOrganizationRole) x).ParentOrganizationRoleId == childId))
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
        //    if (departmentRoleRelation is ParentDepartmentChildOrganizationRole department2)
        //    {
        //        return department2.ChildOrganizationRoleId;
        //    }
        //    if (departmentRoleRelation is ParentOrganizationRoleChildDepartment orgRole1)
        //    {
        //        return orgRole1.ChildDepartmentId;
        //    }
        //    if (departmentRoleRelation is ParentOrganizationRoleChildOrganizationRole orgRole2)
        //    {
        //        return orgRole2.ChildOrganizationRoleId;
        //    }

        //    return 0;
        //}
    }
}