using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.ValidateTestType
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPES_GENERAL_ACCESS)]

    public class ValidateTestTypeQuery : IRequest<Unit>
    {
        public int TestTypeId { get; set; }
    }
}
