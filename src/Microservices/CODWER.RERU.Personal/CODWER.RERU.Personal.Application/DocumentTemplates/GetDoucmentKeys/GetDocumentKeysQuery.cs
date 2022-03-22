using CODWER.RERU.Personal.DataTransferObjects.Documents;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.GetDoucmentKeys
{
    public class GetDocumentKeysQuery : IRequest<List<DocumentTemplateCategoryDto>>
    {
    }
}
