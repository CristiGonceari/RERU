using CODWER.RERU.Core.Application.References.GetCandidateCitizenshipes;
using CODWER.RERU.Core.Application.References.GetCandidateNationalites;
using CODWER.RERU.Core.Application.References.GetMaterialStatusType;
using CODWER.RERU.Core.Application.References.GetModernLanguages;
using CODWER.RERU.Core.Application.References.GetStudyTypes;
using CODWER.RERU.Core.Application.References.GetUserProcess;
using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.EnumConverters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RERU.Data.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.References.GetCoreRoles;
using CVU.ERP.Module.Application.ImportProcessServices.ImportProcessModels;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceController : BaseController
    {
        public ReferenceController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("processes-value/select-values")]
        public async Task<List<ProcessDataDto>> GetProcesses()
        {
            var query = new GetUserProcessQuery();

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
        public async Task<List<SelectItem>> GetCoreRoles()
        {
            var query = new GetCoreRolesQuery();

            return await Mediator.Send(query);
        }
    }
}
