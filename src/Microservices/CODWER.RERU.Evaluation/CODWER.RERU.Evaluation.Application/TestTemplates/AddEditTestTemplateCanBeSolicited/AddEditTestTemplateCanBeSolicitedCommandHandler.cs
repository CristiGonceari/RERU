using MediatR;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddEditTestTemplateCanBeSolicited
{
    public class AddEditTestTemplateCanBeSolicitedCommandHandler : IRequestHandler<AddEditTestTemplateCanBeSolicitedCommand, int>
    {
        private readonly AppDbContext _appDbContext;

        public AddEditTestTemplateCanBeSolicitedCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(AddEditTestTemplateCanBeSolicitedCommand request, CancellationToken cancellationToken)
        {
            var testTemplate = _appDbContext.TestTemplates.First(x => x.Id == request.TestTemplateId);

            testTemplate.CanBeSolicited = testTemplate.CanBeSolicited switch
            {
                null => true,
                true => false,
                false => true
            };

            await _appDbContext.SaveChangesAsync();

            return testTemplate.Id;
        }
    }
}
