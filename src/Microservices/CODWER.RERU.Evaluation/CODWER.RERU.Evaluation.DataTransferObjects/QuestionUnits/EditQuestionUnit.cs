﻿using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.StorageService.Models;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits
{
    public class EditQuestionUnitDto
    {
        public int Id { get; set; }
        public int QuestionCategoryId { get; set; }
        public string Question { get; set; }
        public List<string> Tags { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public QuestionUnitStatusEnum Status { get; set; }
        public int QuestionPoints { get; set; }
        public AddFileDto FileDto { get; set; }
        public string? MediaFileId { get; set; }
    }
}
