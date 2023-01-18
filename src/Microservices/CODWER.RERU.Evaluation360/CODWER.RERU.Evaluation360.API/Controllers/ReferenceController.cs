using CODWER.RERU.Evaluation360.API.Config;
using Microsoft.AspNetCore.Mvc;
using CODWER.RERU.Evaluation360.Application.References.GetDepartmentsValue;
using CODWER.RERU.Evaluation360.Application.References.GetRolesValue;
using System.Threading.Tasks;
using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.EnumConverters;
using CVU.ERP.Common.DataTransferObjects.Users;

namespace CODWER.RERU.Evaluation360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceController : BaseController
    {
        [HttpGet("departments/select-items")]
        public async Task<List<SelectItem>> GetDepartments()
        {
            var query = new GetDepartmentsValuesQuery();

            return await Sender.Send(query);
        }
        [HttpGet("roles/select-items")]
        public async Task<List<SelectItem>> GetRoles()
        {
            var query = new GetRolesValuesQuery();

            return await Sender.Send(query);
        }
        [HttpGet("user-status/select-items")]
        public Task<UserStatusEnum> GetUserEnum()
        {
            var items = UserStatusEnum.Employee;

            return Task.FromResult(items);
        }
    }
}