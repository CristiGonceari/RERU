using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddTestTemplateRules
{
    public class AddTestTemplateRulesCommandHandler : IRequestHandler<AddTestTemplateRulesCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public AddTestTemplateRulesCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(AddTestTemplateRulesCommand request, CancellationToken cancellationToken)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(request.Data.Rules);
            var rulesToAdd = Convert.ToBase64String(plainTextBytes);

            var testTemplate = await _appDbContext.TestTemplates.FirstOrDefaultAsync(x => x.Id == request.Data.TestTemplateId);
            testTemplate.Rules = rulesToAdd;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
