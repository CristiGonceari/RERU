using CODWER.RERU.Evaluation360.API.Config;
using CODWER.RERU.Evaluation360.Application.References.GetDepartmentsValue;
using CODWER.RERU.Evaluation360.Application.References.GetRolesValue;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.DataTransferObjects.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Common.EnumConverters;
using System.Linq;

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
        public async Task<SelectItem> GetUserEnum()
        {
            var items = EnumConverter<UserStatusEnum>.SelectValues;
            var filteredList = items.Where(x => x.Label == UserStatusEnum.Employee.ToString()).First();

            return filteredList;
        }
     }
}