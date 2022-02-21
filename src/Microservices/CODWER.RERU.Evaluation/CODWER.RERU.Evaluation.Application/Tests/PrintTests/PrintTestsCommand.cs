using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Tests.PrintTests
{
    [ModuleOperation(permission: PermissionCodes.TESTS_GENERAL_ACCESS)]
    public class PrintTestsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string testTemplateName { get; set; }
        public string UserName { get; set; }
        public TestStatusEnum? TestStatus { get; set; }
        public string LocationKeyword { get; set; }
        public string EventName { get; set; }
        public string Idnp { get; set; }
        public DateTime? ProgrammedTimeFrom { get; set; }
        public DateTime? ProgrammedTimeTo { get; set; }
    }
}
