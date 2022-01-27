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
using CODWER.RERU.Personal.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using Microsoft.AspNetCore.Mvc;
using CVU.ERP.Common.EnumConverters;

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
        public async Task<List<SelectItem>> GetOrganizationRoles([FromQuery] GetOrganizationRolesSelectValuesQuery query)
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
            var query = new GetOrganizationRolesChartQuery{OrganizationalChartId = chartId};

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
    }
}
