﻿using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTypes
{
    public class AddEditTestTypeDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public int? MinPercent { get; set; }
        public int? Duration { get; set; }
        public TestTypeModeEnum Mode { get; set; }
    }
}