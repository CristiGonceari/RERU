using AutoMapper;
using CVU.ERP.OrganigramService.Enums;
using CVU.ERP.OrganigramService.Models;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;
using RERU.Data.Persistence.Context;

namespace CVU.ERP.OrganigramService.Services.Implementations
{
    public class GetOrganigramService : IGetOrganigramService
    {
        private readonly AppDbContext _appDbContext;

        public GetOrganigramService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<OrganigramHead> GetOrganigramHead()
        {
            var lastActiveOrganigram = _appDbContext.OrganizationalCharts
                .Where(oc => oc.FromDate.Value.Date <= DateTime.Now.Date)
                .OrderBy(oc => oc.FromDate)
                .LastOrDefault();

            OrganigramHead head = new OrganigramHead();

            if (lastActiveOrganigram != null)
            {
                head = await GetHead(lastActiveOrganigram.Id);

            }
            
            return head;
        }

        public async Task<List<OrganigramContent>> GetOrganigramContent(int parentDepartmentId, OrganizationalChartItemType type, int organigramId)
        {
            var listResult = new List<OrganigramContent>();

            switch (type)
            {
                case OrganizationalChartItemType.Department:
                    {
                        var departmentDepartments = _appDbContext.ParentDepartmentChildDepartments
                            .Include(x => x.ChildDepartment)
                            .Where(x => x.OrganizationalChartId == organigramId && x.ParentDepartmentId == parentDepartmentId);

                        var departmentRoles = _appDbContext.ParentDepartmentChildRoles
                            .Include(x => x.ChildRole)
                            .Where(x => x.OrganizationalChartId == organigramId && x.ParentDepartmentId == parentDepartmentId);

                        foreach (var dd in departmentDepartments.ToList())
                        {
                            listResult.Add(new OrganigramContent
                            {
                                Id = dd.ChildDepartment.Id,
                                Name = dd.ChildDepartment.Name,
                                Type = OrganizationalChartItemType.Department,
                                RelationId = dd.Id,
                            });
                        }

                        foreach (var dr in departmentRoles.ToList())
                        {
                            listResult.Add(new OrganigramContent
                            {
                                Id = dr.ChildRole.Id,
                                Name = dr.ChildRole.Name,
                                Type = OrganizationalChartItemType.Role,
                                RelationId = dr.Id,
                            });
                        }

                        break;
                    }

                case OrganizationalChartItemType.Role:
                    {
                        var roleDepartments = _appDbContext.ParentRoleChildDepartments
                            .Include(x => x.ChildDepartment)
                            .Where(x => x.OrganizationalChartId == organigramId && x.ParentRoleId == parentDepartmentId);

                        var roleRoles = _appDbContext.ParentRoleChildRoles
                            .Include(x => x.ChildRole)
                            .Where(x => x.OrganizationalChartId == organigramId && x.ParentRoleId == parentDepartmentId);

                        foreach (var rd in roleDepartments.ToList())
                        {
                            listResult.Add(new OrganigramContent
                            {
                                Id = rd.ChildDepartment.Id,
                                Name = rd.ChildDepartment.Name,
                                Type = OrganizationalChartItemType.Department,
                                RelationId = rd.Id,
                            });
                        }

                        foreach (var rr in roleRoles.ToList())
                        {
                            listResult.Add(new OrganigramContent
                            {
                                Id = rr.ChildRole.Id,
                                Name = rr.ChildRole.Name,
                                Type = OrganizationalChartItemType.Role,
                                RelationId = rr.Id,
                            });
                        }

                        break;
                    }
            }

            return listResult;
        }

        public async Task<List<UserProfile>> GetOrganigramUserProfiles(int id, OrganizationalChartItemType type)
        {
            var userProfiles = _appDbContext.UserProfiles
                .Include(up => up.Department)
                .Include(up => up.Role)
                .AsQueryable();

            if (type == OrganizationalChartItemType.Department)
            {
                userProfiles = userProfiles.Where(p => p.Department.Id == id);
            }
            else if (type == OrganizationalChartItemType.Role)
            {
                userProfiles = userProfiles.Where(p => p.Role.Id == id);

            }

            return userProfiles.ToList();
        }

        private async Task<OrganigramHead> GetHead(int organizationalChartId)
        {
            var result = new OrganigramHead();

            var abstractHead = await _appDbContext.DepartmentRoleRelations.FirstOrDefaultAsync(x =>
                x.OrganizationalChartId == organizationalChartId
                && ((x is ParentDepartmentChildDepartment) && ((ParentDepartmentChildDepartment)x).ParentDepartmentId == null
                    || (x is ParentDepartmentChildRole) && ((ParentDepartmentChildRole)x).ParentDepartmentId == null));

            if (abstractHead is ParentDepartmentChildDepartment)
            {
                var item = await _appDbContext.ParentDepartmentChildDepartments
                    .Include(x => x.ParentDepartment)
                    .Include(x => x.ChildDepartment)
                    .FirstOrDefaultAsync(x => x.Id == abstractHead.Id);

                result.OrganigramId = item.OrganizationalChartId;
                result.OrganigramHeadName = item.ChildDepartment.Name;
                result.ParentDepartmentId = item.ChildDepartmentId;
                result.Type = OrganizationalChartItemType.Department;
            }
            else if (abstractHead is ParentDepartmentChildRole)
            {
                var item = await _appDbContext.ParentDepartmentChildRoles
                    .Include(x => x.ChildRole)
                    .FirstOrDefaultAsync(x => x.Id == abstractHead.Id);

                result.OrganigramId = item.OrganizationalChartId;
                result.OrganigramHeadName = item.ChildRole.Name;
                result.ParentDepartmentId = item.ChildRoleId;
                result.Type = OrganizationalChartItemType.Role;
            }

            return result;
        }

    }
}
