using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions
{
    public class TestCategoryQuestionContentDto
    {
        public SequenceEnum SequenceType { get; set; }
        public int? UsedQuestionCount { get; set; }
        public string QuestionCategoryName { get; set; }
        public List<QuestionUnitDto> Questions { get; set; }
    }
}
