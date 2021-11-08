using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.AddEditTestTypeSettings
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPES_GENERAL_ACCESS)]

    public class AddEditTestTypeSettingsCommand : IRequest<Unit>
    {
        public TestTypeSettingsDto Input { get; set; }
    }

}
