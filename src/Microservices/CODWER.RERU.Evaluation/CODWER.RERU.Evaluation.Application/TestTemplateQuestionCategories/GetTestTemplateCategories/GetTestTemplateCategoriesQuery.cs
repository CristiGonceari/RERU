﻿using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplateQuestionCategories;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.GetTestTemplateCategories
{
    [ModuleOperation(permission: PermissionCodes.TEST_TEMPLATES_GENERAL_ACCESS)]
    public class GetTestTemplateCategoriesQuery : IRequest<List<TestTemplateQuestionCategoryDto>>
    {
        public int TestTemplateId { get; set; }
    }
}