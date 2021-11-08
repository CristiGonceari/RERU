using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.EditTestType
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPES_GENERAL_ACCESS)]

    public class EditTestTypeCommand : IRequest<int>
    {
        public AddEditTestTypeDto Input { get; set; }
    }
}
