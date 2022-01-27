using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorsSelectValues
{
    public class GetContractorsSelectValuesQuery : IRequest<List<SelectItem>>
    {
    }
}
