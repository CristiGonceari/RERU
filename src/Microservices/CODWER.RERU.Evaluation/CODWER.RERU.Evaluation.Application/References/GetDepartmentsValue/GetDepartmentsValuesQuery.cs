using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.References.GetDepartmentsValue
{
    public class GetDepartmentsValuesQuery : IRequest<List<SelectItem>>
    {
    }
}
