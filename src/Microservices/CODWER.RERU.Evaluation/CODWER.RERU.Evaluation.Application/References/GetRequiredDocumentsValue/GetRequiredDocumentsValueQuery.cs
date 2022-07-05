using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.References.GetRequiredDocumentsValue
{
    public class GetRequiredDocumentsValueQuery : IRequest<List<SelectItem>>
    {
        public string Name { get; set; }
    }
}
