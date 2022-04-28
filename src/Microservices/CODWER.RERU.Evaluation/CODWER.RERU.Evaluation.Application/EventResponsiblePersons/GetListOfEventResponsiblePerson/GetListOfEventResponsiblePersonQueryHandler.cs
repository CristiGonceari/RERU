using MediatR;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetListOfEventResponsiblePerson
{
    public class GetListOfEventResponsiblePersonQueryHandler : IRequestHandler<GetListOfEventResponsiblePersonQuery, List<int>>
    {
        private readonly AppDbContext _appDbContext;

        public GetListOfEventResponsiblePersonQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<int>> Handle(GetListOfEventResponsiblePersonQuery request, CancellationToken cancellationToken)
        {
            var eventResponsiblePerson = _appDbContext.EventResponsiblePersons
                                                        .Where(erp => erp.EventId == request.EventId)
                                                        .Select(erp => erp.UserProfileId)
                                                        .ToList();

            return eventResponsiblePerson;
        }
    }
}
