using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.StorageService.Models;
using MediatR;
using System.Collections.Generic;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.AddQuestionUnit
{
    [ModuleOperation(permission: PermissionCodes.QUESTION_UNITS_GENERAL_ACCESS)]

    public class AddQuestionUnitCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int QuestionCategoryId { get; set; }
        public string Question { get; set; }
        public List<string> Tags { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public QuestionUnitStatusEnum Status { get; set; }
        public int QuestionPoints { get; set; }
        public AddFileDto FileDto { get; set; }
    }
}
