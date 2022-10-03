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
                NewItem(PermissionCodes.ACCES_GENERAL_LA_ADRESA, "General permissions for Address Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_ATESTARI, "General permissions for Attestation Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_BADGE, "General permissions for Badge Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_BONUSURI, "General permissions for Bonus Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_CONTACTE, "General permissions for Contractor_Custom_Field_Value Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_VALORI_COSTUMIZATE_DE_CONTRACTOR, "General permissions for Contractor_Driver_License_Category Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_DEPARTAMENTE_DE_CONTRACTOR, "General permissions for Contractor_Department Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_LICENTA_DE_CONDUCERE_AL_CONTRACOTRULUI, "General permissions for Contractor_Driver_License_Category Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_EVENIMENTE_DE_CONTRACTOR, "General permissions for Contractor_Event Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_LIMBI_DE_CONTRACTOR, "General permissions for Contractor_Languages Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_CONTRATORI, "General permissions for Contractor Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_COLOANE_COSTUMIZATE_DE_CONTRACTOR, "General permissions for Contractor_Type_Custom_Field Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_TIPUL_CONTRATORILOR, "General permissions for Contractor_Type Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_RELATIA_DEPARTAMENT_ROL, "General permissions for Department_Role_Relation Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_DEPARTAMENTE, "General permissions for Department Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_NOMENCLATOARE, "General permissions for Nomenclature Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_ROLURI, "General permissions for Organization_Role Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_PENALIZARI, "General permissions for Penalization Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_POZITII, "General permissions for Position Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_RANKURI, "General permissions for Rank Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_VACANTE, "General permissions for Vacation Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_ORGANIGRAMA, "General permissions for Organizational_Chart Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_PROFILUL_UTILIZATORULUI, "General permissions for User_Profile Entity"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_COMPONENTA_FAMILIEI, "General permissions for Family Components"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_SEMNAREA_DOCUMENTELOR, "General permissions for Sign Documents"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_DOCUMENTELE_CONTRACTORILOR, "General permissions for manage contractor files"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_CEREREA_DEMISIEI, "General permissions for dismiss contractor"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_TABELA_DE_PONTAJ, "General permissions for Time Sheet Table"),
                NewItem(PermissionCodes.ACCESS_GENERAL_LA_SABLOANE_DE_DOCUMENTE, "General permissions for Document Templates"),
            }.ToArray());
        }

        private ModulePermission NewItem(string code, string description)
        {
            return new() {Code = code, Description = description};
        }
    }
}
