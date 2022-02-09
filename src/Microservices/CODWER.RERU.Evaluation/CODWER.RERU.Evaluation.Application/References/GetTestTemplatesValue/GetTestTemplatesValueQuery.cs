using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.References.GetTestTemplatesValue
{
    public class GetTestTemplatesValueQuery : IRequest<List<SelectItem>>
    {
    }
}
