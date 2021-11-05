using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;

namespace CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits
{
    public class AddQuestionUnitExcelDto
    {
        public int Row { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public AddEditQuestionUnitDto QuestionUnitDto { get; set; }
        public bool Error { get; set; }
        public List<AddOptionExcelDto> AddOptions { get; set; }
    }
}
