using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.References.GetRequiredDocumentsValue
{
    public class GetRequiredDocumentsValueQuery : IRequest<List<SelectItem>>
    {
    }
}
