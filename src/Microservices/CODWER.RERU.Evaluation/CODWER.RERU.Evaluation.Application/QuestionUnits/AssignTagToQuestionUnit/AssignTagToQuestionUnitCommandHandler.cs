using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.AssignTagToQuestionUnit
{
    public class AssignTagToQuestionUnitCommandHandler : IRequestHandler<AssignTagToQuestionUnitCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public AssignTagToQuestionUnitCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(AssignTagToQuestionUnitCommand request, CancellationToken cancellationToken)
        {
            var questionUnitId = await _appDbContext.QuestionUnits
                .Include(x => x.QuestionUnitTags)
                    .ThenInclude(x => x.Tag)
                .FirstAsync(x => x.Id == request.QuestionUnitId);

            var existingQuestionUnitTags = questionUnitId?.QuestionUnitTags;
            var questionUnitTagsToAdd = new List<QuestionUnitTag>();

            if (existingQuestionUnitTags != null && existingQuestionUnitTags.Count > 0)
            {
                var tagsToDelete = new List<QuestionUnitTag>();

                foreach (var existingTag in existingQuestionUnitTags)
                {
                    if (!request.Tags.Contains(existingTag.Tag.Name))
                    {
                        tagsToDelete.Add(existingTag);
                    }
                }

                if (tagsToDelete.Count > 0)
                {
                    _appDbContext.QuestionUnitTags.RemoveRange(tagsToDelete);
                    await _appDbContext.SaveChangesAsync();

                    foreach (var tagToDetach in tagsToDelete)
                    {
                        existingQuestionUnitTags.Remove(tagToDetach);
                    }
                }
            }

            if (request.Tags != null)
            {
                foreach (var tag in request.Tags)
                {
                    if (existingQuestionUnitTags.Any(x => x.Tag.Name.Equals(tag)))
                    {
                        continue;
                    }

                    var existingTag = _appDbContext.Tags.FirstOrDefault(x => x.Name.Equals(tag));

                    if (existingTag == null)
                    {
                        existingTag = new Tag() { Name = tag };

                        await _appDbContext.Tags.AddAsync(existingTag);
                        await _appDbContext.SaveChangesAsync();
                    }

                    questionUnitTagsToAdd.Add(new QuestionUnitTag()
                    {
                        QuestionUnitId = request.QuestionUnitId,
                        TagId = existingTag.Id
                    });
                }

            }


            if (questionUnitTagsToAdd.Count > 0)
            {
                await _appDbContext.QuestionUnitTags.AddRangeAsync(questionUnitTagsToAdd);
                await _appDbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }

}
