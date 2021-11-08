using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.ValidateTestType
{
    public class ValidateTestTypeQueryHandler : IRequestHandler<ValidateTestTypeQuery, Unit>
    {
        public ValidateTestTypeQueryHandler()
        {
        }

        public async Task<Unit> Handle(ValidateTestTypeQuery request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
