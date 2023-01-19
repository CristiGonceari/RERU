using CODWER.RERU.Evaluation360.API.Config;
using Microsoft.AspNetCore.Mvc;
using CODWER.RERU.Evaluation360.Application.References.GetDepartmentsValue;
using CODWER.RERU.Evaluation360.Application.References.GetRolesValue;
using System.Threading.Tasks;
using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.DataTransferObjects.Users;
using System;
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

            return items;
        }

        public static class EnumConverter<TEnum> where TEnum : Enum
        {
            public static SelectItem SelectValues
            {
                get
                {
                    return Enum.GetValues(typeof(TEnum)).OfType<TEnum>().ToList()
                        .Select(item => new SelectItem
                            {
                                Label = item.ToString(),
                                Value = Convert.ToInt32(item).ToString()
                            })
                        .OrderBy(i => i.Label)
                        .Where(i => i.Label == "Employee")
                        .FirstOrDefault();
                }
            }
        }
    }
}