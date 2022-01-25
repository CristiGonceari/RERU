using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorEmails
{
    public class GetContractorEmailQuery : IRequest<List<SelectItem>>
    {
        public int Id { get; set; }
    }
}
