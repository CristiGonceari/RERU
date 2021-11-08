using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.References.GetQuestionCategoryValue
{
    public class GetQuestionCategoryValueQuery : IRequest<List<SelectItem>>
    {
    }
}
