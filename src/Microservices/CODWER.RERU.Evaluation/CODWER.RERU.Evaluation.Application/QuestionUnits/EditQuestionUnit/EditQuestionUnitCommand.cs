using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionUnit
{
    [ModuleOperation(permission: PermissionCodes.QUESTION_UNITS_GENERAL_ACCESS)]

    public class EditQuestionUnitCommand : IRequest<Unit>
    {
        public EditQuestionUnitDto Data { get; set; }

    }
}
