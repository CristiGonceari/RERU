using MediatR;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionEmailMessages.GetSolicitedVacantPositionEmailMessage
{
    public class GetSolicitedVacantPositionEmailMessageQueryHandler : IRequestHandler<GetSolicitedVacantPositionEmailMessageQuery, string>
    {
        private readonly AppDbContext _appDbContext;

        public GetSolicitedVacantPositionEmailMessageQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> Handle(GetSolicitedVacantPositionEmailMessageQuery request, CancellationToken cancellationToken)
        {
            var item =
                _appDbContext.SolicitedVacantPositionEmailMessages.First(x => x.MessageType == request.MessageType);

            return  item.Message;
        }
    }
}
