﻿using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits
{
    public class QuestionUnitDto
    {
        public int Id { get; set; }
        public QuestionUnitStatusEnum Status { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public int AnswersCount { get; set; }
        public int UsedInTestsCount { get; set; }
        public string Question { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public List<string> Tags { get; set; }
        public int QuestionPoints { get; set; }
    }
}
