using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEditEvaluation
{
    public class GetEditEvaluationQueryHandler : IRequestHandler<GetEditEvaluationQuery, GetEvaluationDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public GetEditEvaluationQueryHandler(
            AppDbContext dbContext, 
            IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<GetEvaluationDto> Handle(GetEditEvaluationQuery request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations
                .Include(e => e.EvaluatedUserProfile)
                    .ThenInclude(r => r.EmployeeFunction)
                .Include(e => e.EvaluatorUserProfile)
                    .ThenInclude(r => r.EmployeeFunction)
                .Include(e => e.CounterSignerUserProfile)
                    .ThenInclude(r => r.EmployeeFunction)
                .OrderByDescending(e => e.CreateDate)
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            Test test1 = await GetPointsTest(evaluation.EvaluatedUserProfileId, BasicTestTemplateEnum.PregatireGenerala);
            Test test2 = await GetPointsTest(evaluation.EvaluatedUserProfileId, BasicTestTemplateEnum.PregatireDeSpecialitate);
            Test test3 = await GetPointsTest(evaluation.EvaluatedUserProfileId, BasicTestTemplateEnum.InstructiaTragerii);
            Test test4 = await GetPointsTest(evaluation.EvaluatedUserProfileId, BasicTestTemplateEnum.InterventiaProfesionala);

            if (test1 != null)
            {
                evaluation.Question9 = ConvertToPoints(test1.AccumulatedPercentage);
            }
            if (test2 != null)
            {
                evaluation.Question10 = ConvertToPoints(test2.AccumulatedPercentage);
            }
            if (test3 != null)
            {
                evaluation.Question11 = ConvertToPoints(test3.AccumulatedPercentage);
            }
            if (test4 != null)
            {
                evaluation.Question12 = ConvertToPoints(test4.AccumulatedPercentage);
            }

            await _dbContext.SaveChangesAsync();
            
            return _mapper.Map<GetEvaluationDto>(evaluation);
        }

        private async Task<Test> GetPointsTest(int evaluatedUserProfileId, BasicTestTemplateEnum basicTestTemplate)
        {
            return await _dbContext.Tests
                        .Include(e => e.TestTemplate)
                        .Where(e => e.UserProfileId == evaluatedUserProfileId &&
                                    e.TestTemplate.BasicTestTemplate == basicTestTemplate)
                        .OrderByDescending(e => e.CreateDate)
                        .FirstOrDefaultAsync();
        }

        private int ConvertToPoints(int? accumulatedPercentage)
        {
            if (accumulatedPercentage >= 0 && accumulatedPercentage <= 25) return 1;
            else if (accumulatedPercentage > 25 && accumulatedPercentage <= 50) return 2;
            else if (accumulatedPercentage > 50 && accumulatedPercentage <= 75) return 3;
            else if (accumulatedPercentage > 75 && accumulatedPercentage <= 100) return 4;
            else throw new ArgumentOutOfRangeException("accumulatedPercentage must be between 0 and 100.");
        }
    }
}