using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Contractors.GetContractorEmails;
using CODWER.RERU.Personal.Application.Contractors.GetContractorsSelectValues;
using CODWER.RERU.Personal.Application.Contractors.GetSuperiorContractorsSelectValues;
using CODWER.RERU.Personal.Application.Departments.GetDepartmentsSelectValues;
using CODWER.RERU.Personal.Application.Enums;
using CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.GetNomenclatureRecordsSelectValues;
using CODWER.RERU.Personal.Application.OrganizationalCharts.GetDepartments;
using CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationRoles;
using CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRolesSelectValues;
using RERU.Data.Entities.PersonalEntities.Enums;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using Microsoft.AspNetCore.Mvc;
using CVU.ERP.Common.EnumConverters;
using RERU.Data.Entities.Enums;
using CODWER.RERU.Personal.DataTransferObjects.SelectValues;
using CODWER.RERU.Personal.Application.References.GetCandidateCitizenshipes;
using CODWER.RERU.Personal.Application.References.GetCandidateNationalites;
using CODWER.RERU.Personal.Application.References.GetEmployeeFunctions;
using CODWER.RERU.Personal.Application.References.GetStudyTypes;
using CODWER.RERU.Personal.Application.References.GetModernLanguages;
using CODWER.RERU.Personal.Application.References.GetMaterialStatusType;
using CODWER.RERU.Personal.Application.References.GetPersonalRoles;
using Microsoft.AspNetCore.Authorization;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceController : BaseController
    {
        //[HttpGet("nomenclature-types/select-values")]
        //public List<SelectItem> GetNomenclatureTypesSelectValues()
        //{
        //    var items = EnumConverter<NomenclatureTypeEnum>.SelectValues;

        //    return items;
        //}

        //[HttpGet("nomenclature/{nomenclatureType}/select-values")]
        //public async Task<List<SelectItem>> GetNomenclatureSelectValues([FromRoute] NomenclatureTypeEnum nomenclatureType)
        //{
        //    var query = new GetNomenclatureSelectValuesQuery{NomenclatureType = nomenclatureType};

        //    return await Mediator.Send(query);
        //}

        //[HttpGet("nomenclature-types/select-values")]
        //public async Task<IEnumerable<SelectItem>> GetNomenclatureTypesSelectValues()
        //{
        //    var query = new GetNomenclatureSelectValuesQuery();

        //    return await Mediator.Send(query);
        //}

        [HttpGet("nomenclature-records/select-values")]
        public async Task<IEnumerable<SelectItem>> GetNomenclatureSelectValues([FromQuery] GetNomenclatureRecordsSelectValuesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("contractors/select-values/{contractorId}")]
        public async Task<List<SelectItem>> GetContractorTypes([FromRoute] int contractorId)
        {
            var query = new GetSuperiorContractorsSelectValuesQuery{ContractorId = contractorId};

            return await Mediator.Send(query);
        }

        [HttpGet("contractors/select-values")]
        public async Task<List<SelectItem>> GetSuperiorContractorTypes([FromRoute] int contractorId)
        {
            var query = new GetContractorsSelectValuesQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("organization-roles/select-values")]
        public async Task<List<SelectItem>> GetOrganizationRoles([FromQuery] GetRolesSelectValuesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("departments/select-values")]
        public async Task<List<SelectItem>> GetDepartments()
        {
            var query = new GetDepartmentsSelectValuesQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("sex-types/select-values")]
        public List<SelectItem> GetSexTypes()
        {
            var items = EnumConverter<SexTypeEnum>.SelectValues;

            return items;
        }

        [HttpGet("contact-types/select-values")]
        public List<SelectItem> GetContactTypes()
        {
            var items = EnumConverter<ContactTypeEnum>.SelectValues;

            return items;
        }

        [HttpGet("employer-states/select-values")]
        public List<SelectItem> GetEmployerStates()
        {
            var items = EnumConverter<EmployersStateEnum>.SelectValues;

            return items;
        }


        [HttpGet("organization-roles/chart/{chartId}")]
        public async Task<List<SelectItem>> GetOrganizationRolesForChart([FromRoute] int chartId)
        {
            var query = new GetRolesChartQuery{OrganizationalChartId = chartId};

            return await Mediator.Send(query);
        }

        [HttpGet("departments/chart/{chartId}")]
        public async Task<List<SelectItem>> GetDepartmentsForChart([FromRoute] int chartId)
        {
            var query = new GetDepartmentsChartQuery { OrganizationalChartId = chartId };

            return await Mediator.Send(query);
        }

        [HttpGet("time-sheet/select-values")]
        public List<SelectItem> GetTimeSheet()
        {
            var items = EnumConverter<TimeSheetValueEnum>.SelectValues
                .Select(x => new SelectItem
                {
                    Label = int.Parse(x.Value) < 100 ? x.Value : x.Label,
                    Value = x.Value
                })
                .OrderBy(x => int.Parse(x.Value))
                .ToList();

            return items;

        }

        [HttpGet("emails/{contractorId}")]
        public async Task<List<SelectItem>> GetContractorEmails([FromRoute] int contractorId)
        {
            var query = new GetContractorEmailQuery { Id = contractorId };

            return await Mediator.Send(query);
        }




        [HttpGet("candidate-citizens/select-values")]
        public async Task<List<SelectValue>> GetCandidateCitizens()
        {
            var query = new GetCandidateCitizenshipesQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("candidate-nationalities/select-values")]
        public async Task<List<SelectValue>> GetCandidateNationalities()
        {
            var query = new GetCandidateNationalitesQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("employee-functions/select-values")]
        public async Task<List<SelectItem>> GetEmployeeFunctions()
        {
            var query = new GetEmployeeFunctionsQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("candidate-sex/select-values")]
        public async Task<List<SelectItem>> GetSexEnum()
        {
            var items = EnumConverter<SexTypeEnum>.SelectValues;

            return items;
        }

        [HttpGet("candidate-state-language-level/select-values")]
        public async Task<List<SelectItem>> GetStateLanguageLevelEnum()
        {
            var items = EnumConverter<StateLanguageLevel>.SelectValues;

            return items;
        }

        [HttpGet("registration-flux-steps/select-values")]
        public async Task<List<SelectItem>> GetRegistrationFluxStepsEnum()
        {
            var items = EnumConverter<RegistrationFluxStepEnum>.SelectValues;

            return items;
        }

        [HttpGet("studies-frequency/select-values")]
        public async Task<List<SelectItem>> GetStudyFrequencyEnum()
        {
            var items = EnumConverter<StudyFrequencyEnum>.SelectValues;

            return items;
        }

        [HttpGet("study-types/select-values")]
        public async Task<List<SelectValue>> GetStudyTypes()
        {
            var query = new GetStudyTypesQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("modern-languages/select-values")]
        public async Task<List<SelectValue>> GetModernLanguages()
        {
            var query = new GetModernLanguageQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("knowledge-quelifiers/select-values")]
        public async Task<List<SelectItem>> GetKnowledgeQuelifiersEnum()
        {
            var items = EnumConverter<KnowledgeQuelifiersEnum>.SelectValues;

            return items;
        }

        [HttpGet("material-status-type/select-values")]
        public async Task<List<SelectValue>> GetMaterialStatusType()
        {
            var query = new GetMaterialStatusTypeQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("kinship-degree/select-values")]
        public async Task<List<SelectItem>> GetKinshipDegreeEnum()
        {
            var items = EnumConverter<KinshipDegreeEnum>.SelectValues;

            return items;
        }

        [HttpGet("military-obligation-type-enum/select-values")]
        public async Task<List<SelectItem>> GetMilitaryObligationTypeEnum()
        {
            var items = EnumConverter<MilitaryObligationTypeEnum>.SelectValues;

            return items;
        }

        [HttpGet("article-roles/select-values")]
        public async Task<List<SelectItem>> GetPersonalRoles()
        {
            var query = new GetPersonalRolesQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("study-profiles/select-values")]
        public async Task<List<SelectItem>> GetStudyProfilesEnum()
        {
            var items = EnumConverter<StudyProfileEnum>.SelectValues;

            return items;
        }

        [HttpGet("study-courses/select-values")]
        public async Task<List<SelectItem>> GetStudyCoursesEnum()
        {
            var items = EnumConverter<StudyCourseType>.SelectValues;

            return items;
        }
    }
}
