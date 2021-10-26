using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Modules.DeleteModule;
using CODWER.RERU.Core.Application.Modules.EditModule;
using CODWER.RERU.Core.Application.Modules.GetAllModules;
using CODWER.RERU.Core.Application.Modules.GetEditModule;
using CODWER.RERU.Core.Application.Modules.GetModuleDetails;
using CODWER.RERU.Core.Application.Modules.RegisterModule;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Core.API.Controllers.Admin 
{

    [Route ("api/admin/module")]
    public class AdminModuleController : BaseController
    {
        public AdminModuleController (IMediator mediator) : base (mediator) { }

        [HttpPost]
        public async Task RegisterModule ([FromBody] AddEditModuleDto module) 
        {
            var registerModuleCommand = new RegisterModuleCommand { Module = module };
            await Mediator.Send (registerModuleCommand);
        }

        [HttpGet]
        public async Task<PaginatedModel<ModuleDto>> GetUsers ([FromQuery] GetAllModulesQuery query)
        {
            var mediatorResponse = await Mediator.Send (query);
            return mediatorResponse;
        }

        [HttpGet ("{id:int}")]
        public Task<ModuleDto> GetModuleDetails ([FromRoute] int id)
        {
            var getModuleDetails = new GetModuleDetailsQuery (id);
            return Mediator.Send (getModuleDetails);
        }

        [HttpGet ("{id}/edit")]
        public Task<ModuleDto> GetEditModule ([FromRoute] int id) 
        {
            var getEditModule = new GetEditModuleQuery (id);
            return Mediator.Send (getEditModule);
        }

        [HttpPut]
        public Task EditModule ([FromBody] AddEditModuleDto module) 
        {
            var getEditModule = new EditModuleCommand (module);
            return Mediator.Send (getEditModule);
        }

        [HttpDelete ("{id}")]
        public Task DeleteModule ([FromRoute] int id) 
        {
            var deleteCommand = new DeleteModuleCommand (id);
            return Mediator.Send (deleteCommand);
        }
    }
}