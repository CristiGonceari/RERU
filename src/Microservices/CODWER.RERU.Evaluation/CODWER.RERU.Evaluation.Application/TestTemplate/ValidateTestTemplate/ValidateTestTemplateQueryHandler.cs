using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.ValidateTestType
{
    public class ValidateTestTemplateQueryHandler : IRequestHandler<ValidateTestTemplateQuery, Unit>
    {
        public ValidateTestTemplateQueryHandler()
        {
        }

        public async Task<Unit> Handle(ValidateTestTemplateQuery request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
