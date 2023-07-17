/*using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.GetPollResult
{
    public class GetPollResultQueryHandler : IRequestHandler<GetPollResultQuery, PollResultDto>
    {
        private readonly AppDbContext _appDbContext;

        public GetPollResultQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PollResultDto> Handle(GetPollResultQuery request, CancellationToken cancellationToken)
        {
            var allTests = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .Include(x => x.Event)
                    .ThenInclude(x => x.EventUsers)
                .Include(x => x.TestQuestions)
                    .ThenInclude(x => x.QuestionUnit)
                        .ThenInclude(x => x.Options)
                .Include(x => x.TestQuestions)
                    .ThenInclude(x => x.TestQuestionsTestAnswers)
                        .ThenInclude(x => x.TestAnswer)
                .Where(x => x.TestTemplateId == request.TestTemplateId)
                .ToListAsync();

            var eventTestTemplate = await _appDbContext.EventTestTemplates.Include(x => x.TestTemplate).Include(x => x.Event).ThenInclude(x => x.EventUsers).FirstAsync(x => x.TestTemplateId == request.TestTemplateId);
            var thisEvent = eventTestTemplate.Event;
            var totalPollInvited = thisEvent.EventUsers?.Count;
            var totalPollVoted = allTests.Count();
            var testTemplate = eventTestTemplate.TestTemplate;
            var testEvent = eventTestTemplate.Event;

            var questions = allTests.SelectMany(x => x.TestQuestions)
                .GroupBy(x => x.QuestionUnitId)
                .SelectMany(x => x)
                .OrderBy(x => x.QuestionUnitId)
                .ToList();

            var questionsCount = questions.GroupBy(x => x.QuestionUnitId).Select(x => x.FirstOrDefault()).Count();

            var answer = new PollResultDto()
            {
                Id = request.TestTemplateId,
                TestTemplateName = testTemplate.Name,
                EventName = testEvent.Name,
                TotalInvited = totalPollInvited,
                TotalVotedCount = totalPollVoted,
                TotalVotedPercent = totalPollInvited == 0 ? 0 : (double)(totalPollVoted * 100 / totalPollInvited),
                ItemsCount = questionsCount,
                StartDate = thisEvent.FromDate,
                EndDate = thisEvent.TillDate
            };


            answer.Questions = new List<PollQuestionDto>();
            var questionIndex = 1;
            foreach (var question in questions)
            {
                var questionVotedCount = allTests.Select(q => q.TestQuestions.Where(tq => tq.AnswerStatus == AnswerStatusEnum.Answered && tq.QuestionUnitId == question.QuestionUnitId))?.Count();
                var thisQuestion = new PollQuestionDto()
                {
                    Question = question.QuestionUnit.Question,
                    QuestionId = question.QuestionUnitId,
                    Index = questionIndex,
                    VotedCount = questionVotedCount,
                    VotedPercent = totalPollInvited == 0 ? 0 : (double)(questionVotedCount * 100 / totalPollInvited),
                    Options = new List<PollOptionDto>(),

                };

                foreach (var option in question.QuestionUnit.Options)
                {
                    //var votedOptionCount = questions.Where(tq => tq.TestAnswers.Any(a => a.OptionId == option.Id))?.Count();

                    var votedOptionCount = questions.Where(tq => tq.TestQuestionsTestAnswers
                            .Select(x => x.TestAnswer)
                            .ToList()
                            .Any(a => a.OptionId == option.Id))?
                        .Count();

                    var thisOption = new PollOptionDto()
                    {
                        Answer = option.Answer,
                        OptionId = option.Id,
                        VotedCount = votedOptionCount,
                        VotedPercent = questionVotedCount == 0 ? 0 : (double)(votedOptionCount * 100 / questionVotedCount)
                    };

                    thisQuestion.Options.Add(thisOption);
                }

                if (thisQuestion.Index == request.Index)
                {
                    answer.Questions.Add(thisQuestion);
                    return answer;
                }

                questionIndex++;
            }

            return answer;
        }
    }
}
*/