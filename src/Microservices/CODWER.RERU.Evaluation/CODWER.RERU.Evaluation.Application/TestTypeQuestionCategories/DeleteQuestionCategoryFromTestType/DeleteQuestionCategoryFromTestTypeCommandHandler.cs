using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.DeleteQuestionCategoryFromTestType
{
    public class DeleteQuestionCategoryFromTestTypeCommandHandler : IRequestHandler<DeleteQuestionCategoryFromTestTypeCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteQuestionCategoryFromTestTypeCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteQuestionCategoryFromTestTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.TestTypeQuestionCategories.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.TestTypeQuestionCategories.Remove(result);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
