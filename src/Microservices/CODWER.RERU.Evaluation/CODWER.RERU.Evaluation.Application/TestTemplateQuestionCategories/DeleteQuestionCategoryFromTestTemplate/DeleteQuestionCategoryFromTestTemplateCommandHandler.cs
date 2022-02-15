using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.DeleteQuestionCategoryFromTestTemplate
{
    public class DeleteQuestionCategoryFromTestTemplateCommandHandler : IRequestHandler<DeleteQuestionCategoryFromTestTemplateCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteQuestionCategoryFromTestTemplateCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteQuestionCategoryFromTestTemplateCommand request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.TestTypeQuestionCategories.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.TestTypeQuestionCategories.Remove(result);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
