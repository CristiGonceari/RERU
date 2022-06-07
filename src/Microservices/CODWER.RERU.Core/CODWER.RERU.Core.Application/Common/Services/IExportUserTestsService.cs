using CODWER.RERU.Core.DataTransferObjects.Files;
using CODWER.RERU.Core.DataTransferObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Common.Services
{
    public interface IExportUserTestsService
    {
        public Task<ExportExcel> DonwloadUserTestsExcel(List<UserTestDto> data);
    }
}
