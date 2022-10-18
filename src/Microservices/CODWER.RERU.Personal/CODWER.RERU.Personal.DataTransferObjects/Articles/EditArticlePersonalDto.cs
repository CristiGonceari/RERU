﻿using System.Collections.Generic;
using CVU.ERP.StorageService.Models;

namespace CODWER.RERU.Personal.DataTransferObjects.Articles
{
    public class EditArticlePersonalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public AddFileDto? FileDto { get; set; }
        public string MediaFileId { get; set; }
        public List<AssignTagsValuesDto> Roles { get; set; }
    }
}
