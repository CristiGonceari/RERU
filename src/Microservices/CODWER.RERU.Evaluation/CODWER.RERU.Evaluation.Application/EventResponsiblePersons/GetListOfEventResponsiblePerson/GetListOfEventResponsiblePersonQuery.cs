using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetListOfEventResponsiblePerson
{
    public class GetListOfEventResponsiblePersonQuery : IRequest<List<int>>
    {
        public int EventId { get; set; }

    }
}
