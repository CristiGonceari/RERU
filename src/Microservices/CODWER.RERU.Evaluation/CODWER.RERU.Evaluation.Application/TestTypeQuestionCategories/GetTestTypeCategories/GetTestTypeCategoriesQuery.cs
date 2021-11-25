using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypeQuestionCategories;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.GetTestTypeCategories
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPES_GENERAL_ACCESS)]
    public class GetTestTypeCategoriesQuery : IRequest<List<TestTypeQuestionCategoryDto>>
    {
        public int TestTypeId { get; set; }
    }
}
