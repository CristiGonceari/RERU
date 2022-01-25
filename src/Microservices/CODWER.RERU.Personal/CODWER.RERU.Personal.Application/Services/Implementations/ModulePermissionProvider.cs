using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.Module.Common.Providers;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class ModulePermissionProvider : IModulePermissionProvider
    {
        public Task<ModulePermission[]> Get()
        {
            return Task.FromResult(new List<ModulePermission>
            {
                NewItem(PermissionCodes.ADDRESS_GENERAL_ACCESS, "General permissions for Address Entity"),
                NewItem(PermissionCodes.ATTESTATIONS_GENERAL_ACCESS, "General permissions for Attestation Entity"),
                NewItem(PermissionCodes.BADGES_GENERAL_ACCESS, "General permissions for Badge Entity"),
                NewItem(PermissionCodes.BONUSES_GENERAL_ACCESS, "General permissions for Bonus Entity"),
                NewItem(PermissionCodes.CONTACTS_GENERAL_ACCESS, "General permissions for Contractor_Custom_Field_Value Entity"),
                NewItem(PermissionCodes.CONTRACTOR_CUSTOM_FIELD_VALUES_GENERAL_ACCESS, "General permissions for Contractor_Driver_License_Category Entity"),
                NewItem(PermissionCodes.CONTRACTOR_DEPARTMENTS_GENERAL_ACCESS, "General permissions for Contractor_Department Entity"),
                NewItem(PermissionCodes.CONTRACTOR_DRIVER_LICENSE_CATEGORIES_GENERAL_ACCESS, "General permissions for Contractor_Driver_License_Category Entity"),
                NewItem(PermissionCodes.CONTRACTOR_EVENTS_GENERAL_ACCESS, "General permissions for Contractor_Event Entity"),
                NewItem(PermissionCodes.CONTRACTOR_LANGUAGES_GENERAL_ACCESS, "General permissions for Contractor_Languages Entity"),
                NewItem(PermissionCodes.CONTRACTORS_GENERAL_ACCESS, "General permissions for Contractor Entity"),
                NewItem(PermissionCodes.CONTRACTOR_TYPE_CUSTOM_FIELDS_GENERAL_ACCESS, "General permissions for Contractor_Type_Custom_Field Entity"),
                NewItem(PermissionCodes.CONTRACTOR_TYPES_GENERAL_ACCESS, "General permissions for Contractor_Type Entity"),
                NewItem(PermissionCodes.DEPARTMENT_ROLE_RELATIONS_GENERAL_ACCESS, "General permissions for Department_Role_Relation Entity"),
                NewItem(PermissionCodes.DEPARTMENTS_GENERAL_ACCESS, "General permissions for Department Entity"),
                NewItem(PermissionCodes.NOMENCLATURES_GENERAL_ACCESS, "General permissions for Nomenclature Entity"),
                NewItem(PermissionCodes.ORGANIZATION_ROLES_GENERAL_ACCESS, "General permissions for Organization_Role Entity"),
                NewItem(PermissionCodes.PENALIZATIONS_GENERAL_ACCESS, "General permissions for Penalization Entity"),
                NewItem(PermissionCodes.POSITIONS_GENERAL_ACCESS, "General permissions for Position Entity"),
                NewItem(PermissionCodes.RANKS_GENERAL_ACCESS, "General permissions for Rank Entity"),
                NewItem(PermissionCodes.VACATIONS_GENERAL_ACCESS, "General permissions for Vacation Entity"),
                NewItem(PermissionCodes.ORGANIZATIONAL_CHART_GENERAL_ACCESS, "General permissions for Organizational_Chart Entity"),
                NewItem(PermissionCodes.USER_PROFILE_GENERAL_ACCESS, "General permissions for User_Profile Entity"),
                NewItem(PermissionCodes.FAMILY_COMPONENT_GENERAL_ACCESS, "General permissions for Family Components"),
                NewItem(PermissionCodes.SIGN_DOCUMENTS_GENERAL_ACCESS, "General permissions for Sign Documents"),
                NewItem(PermissionCodes.CONTRACTOR_FILE_GENERAL_ACCESS, "General permissions for manage contractor files"),
                NewItem(PermissionCodes.DISMISSAL_REQUEST_GENERAL_ACCESS, "General permissions for dismiss contractor"),
                NewItem(PermissionCodes.TIME_SHEET_TABLE_GENERAL_ACCESS, "General permissions for Time Sheet Table"),
                NewItem(PermissionCodes.DOCUMENTS_TEMPLATE_GENERAL_ACCESS, "General permissions for Document Templates"),
            }.ToArray());
        }

        private ModulePermission NewItem(string code, string description)
        {
            return new() {Code = code, Description = description};
        }
    }
}
