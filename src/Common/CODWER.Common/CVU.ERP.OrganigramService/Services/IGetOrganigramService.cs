using CVU.ERP.OrganigramService.Enums;
using CVU.ERP.OrganigramService.Models;
using RERU.Data.Entities;

namespace CVU.ERP.OrganigramService.Services
{
    public interface IGetOrganigramService
    {
        public Task<OrganigramHead> GetOrganigramHead();
        public Task<List<OrganigramContent>> GetOrganigramContent(int parentDepartmentId, OrganizationalChartItemType type, int organigramId);
        public Task<List<UserProfile>> GetOrganigramUserProfiles(int id, OrganizationalChartItemType type);
    }
}
