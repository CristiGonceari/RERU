using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;
using RERU.Data.Entities.Enums;

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
