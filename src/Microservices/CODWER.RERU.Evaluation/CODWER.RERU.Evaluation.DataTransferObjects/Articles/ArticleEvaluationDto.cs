﻿using CVU.ERP.StorageService.Models;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Articles
{
    public class ArticleEvaluationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public AddFileDto? FileDto { get; set; }
        public string MediaFileId { get; set; }
    }
}
