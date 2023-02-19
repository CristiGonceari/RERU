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
using CODWER.RERU.Evaluation360.Application.References.GetEmployeeFunctions;
using CODWER.RERU.Evaluation360.Application.BLL.References.GetEvaluation360Roles;

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

        [HttpGet("employee-functions/select-items")]
        public async Task<List<SelectItem>> GetEmployeFunctions()
        {
            var query = new GetEmployeeFunctionsQuery();

            return await Sender.Send(query);
        }

        [HttpGet("user-status/select-items")]
        public async Task<List<SelectItem>> GetUserEnum()
        {
            var items = EnumConverter<UserStatusEnum>.SelectValues;
            var filteredList = items.Where(x => x.Label == UserStatusEnum.Employee.ToString()).ToList();

            return filteredList;
        }

        [HttpGet("article-roles/select-values")]
        public async Task<List<SelectItem>> GetArticleRoles()
        {
            var query = new GetEvaluation360RolesQuery();

            return await Sender.Send(query);
        }
     }
}