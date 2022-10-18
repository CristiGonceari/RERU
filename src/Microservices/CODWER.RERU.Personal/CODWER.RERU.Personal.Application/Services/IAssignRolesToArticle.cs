﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Personal.DataTransferObjects;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IAssignRolesToArticle
    {
        Task AssignRolesToArticle(List<AssignTagsValuesDto> requiredDocuments, int articleId);
    }
}
