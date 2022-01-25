using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetSuperiorContractorsSelectValues
{
    public class GetSuperiorContractorsSelectValuesQuery : IRequest<List<SelectItem>>
    {
        public int ContractorId { get; set; }
    }
}
