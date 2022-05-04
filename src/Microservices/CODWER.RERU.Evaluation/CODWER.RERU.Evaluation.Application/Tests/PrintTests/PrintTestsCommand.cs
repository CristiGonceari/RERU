using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Tests.PrintTests
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE)]
    public class PrintTestsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string TestTemplateName { get; set; }
        public string UserName { get; set; }
        public TestResultStatusEnum? ResultStatus { get; set; }
        public TestStatusEnum? TestStatus { get; set; }
        public string LocationKeyword { get; set; }
        public string EventName { get; set; }
        public string Idnp { get; set; }
        public DateTime? ProgrammedTimeFrom { get; set; }
        public DateTime? ProgrammedTimeTo { get; set; }
    }
}
