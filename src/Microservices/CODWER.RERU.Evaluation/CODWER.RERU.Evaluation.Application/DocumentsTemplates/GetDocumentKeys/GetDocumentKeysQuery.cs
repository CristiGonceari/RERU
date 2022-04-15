using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;
using RERU.Data.Entities.Enums;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.GetDocumentKeys
{
    public class GetDocumentKeysQuery : IRequest<List<DocumentTemplateKeyDto>>
    {
        public FileTypeEnum fileType { get; set; }
    }
}
