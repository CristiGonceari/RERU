using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.RegistrationPageMessage.GetRegistrationPageMessage
{
    public class GetRegistrationPageMessageQueryHandler : BaseHandler, IRequestHandler<GetRegistrationPageMessageQuery, string>
    {
        public GetRegistrationPageMessageQueryHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider)
        {
        }

        public async Task<string> Handle(GetRegistrationPageMessageQuery request, CancellationToken cancellationToken)
        {
            return AppDbContext.RegistrationPageMessages.Select(x => x.Message).FirstOrDefault();
        }
    }
}
