using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.AddTestTypeRules
{
    public class AddTestTypeRulesCommandHandler : IRequestHandler<AddTestTypeRulesCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public AddTestTypeRulesCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(AddTestTypeRulesCommand request, CancellationToken cancellationToken)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(request.Data.Rules);
            var rulesToAdd = Convert.ToBase64String(plainTextBytes);

            var testType = await _appDbContext.TestTypes.FirstOrDefaultAsync(x => x.Id == request.Data.TestTypeId);
            testType.Rules = rulesToAdd;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
