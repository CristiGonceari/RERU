using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.SetCategoriesSequence
{
    public class SetCategoriesSequenceCommandHandler : IRequestHandler<SetCategoriesSequenceCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public SetCategoriesSequenceCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(SetCategoriesSequenceCommand request, CancellationToken cancellationToken)
        {
            var testTypeQuestionCategories = await _appDbContext.TestTypeQuestionCategories
                .Where(x => x.TestTypeId == request.TestTypeId)
                .ToListAsync();

            var testType = await _appDbContext.TestTemplates.FirstAsync(x => x.Id == request.TestTypeId);

            testType.CategoriesSequence = request.SequenceType;

            if (request.SequenceType == SequenceEnum.Strict)
            {
                foreach (var category in testTypeQuestionCategories)
                {
                    var newData = request.ItemsOrder.First(x => x.Id == category.Id);
                    category.CategoryIndex = newData.Index;
                }
            }
            else
            {
                foreach (var category in testTypeQuestionCategories)
                {
                    category.CategoryIndex = 0;
                }
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
