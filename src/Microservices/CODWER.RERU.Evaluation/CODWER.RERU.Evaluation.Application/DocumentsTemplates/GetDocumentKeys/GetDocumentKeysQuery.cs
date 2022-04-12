using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.GetDocumentCategoryKeys
{
    public class GetDocumentKeysQuery : IRequest<List<DocumentTemplateKeyDto>>
    {
        public FileTypeEnum fileType { get; set; }
    }
}
