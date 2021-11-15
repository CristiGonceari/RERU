using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeByStatus
{
    public class GetTestTypeByStatusQueryHandler : IRequestHandler<GetTestTypeByStatusQuery, List<SelectTestTypeValueDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTypeByStatusQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectTestTypeValueDto>> Handle(GetTestTypeByStatusQuery request, CancellationToken cancellationToken)
        {
            var testTypes = _appDbContext.TestTypes
                .Include(x => x.Settings)
                .Where(x => x.Status == request.TestTypeStatus && x.Mode == (int)TestTypeModeEnum.Test)
                .AsQueryable();

            if (request.EventId.HasValue)
            {
                testTypes = testTypes
                    .Include(x => x.EventTestTypes)
                    .Where(x => x.EventTestTypes.Any(e => e.EventId == request.EventId));
            }

            var onlyOneAnswerTests = testTypes.Select(x => _mapper.Map<SelectTestTypeValueDto>(x)).ToList();

            foreach (var x in onlyOneAnswerTests)
            {
                var testTypeCategories = _appDbContext.TestTypeQuestionCategories.Where(tt => tt.TestTypeId == x.TestTypeId).All(tt => tt.QuestionType == QuestionTypeEnum.OneAnswer);

                if (testTypeCategories)
                {
                    x.IsOnlyOneAnswer = true;
                }
                else x.IsOnlyOneAnswer = false;
            }

            return onlyOneAnswerTests;
        }
    }
}
