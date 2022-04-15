﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

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
            var testTemplateQuestionCategories = await _appDbContext.TestTemplateQuestionCategories
                .Where(x => x.TestTemplateId == request.TestTemplateId)
                .ToListAsync();

            var testTemplate = await _appDbContext.TestTemplates.FirstAsync(x => x.Id == request.TestTemplateId);

            testTemplate.CategoriesSequence = request.SequenceType;

            if (request.SequenceType == SequenceEnum.Strict)
            {
                foreach (var category in testTemplateQuestionCategories)
                {
                    var newData = request.ItemsOrder.First(x => x.Id == category.Id);
                    category.CategoryIndex = newData.Index;
                }
            }
            else
            {
                foreach (var category in testTemplateQuestionCategories)
                {
                    category.CategoryIndex = 0;
                }
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
