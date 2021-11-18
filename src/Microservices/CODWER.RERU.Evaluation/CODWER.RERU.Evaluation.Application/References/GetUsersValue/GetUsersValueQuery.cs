using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.References.GetUsersValue
{
    public class GetUsersValueQuery : IRequest<List<SelectItem>>
    {
        public int? EventId { get; set; }
    }
}
