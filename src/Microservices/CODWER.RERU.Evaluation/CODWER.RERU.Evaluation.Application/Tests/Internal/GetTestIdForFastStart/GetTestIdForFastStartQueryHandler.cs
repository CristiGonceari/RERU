using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.TestDatas;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.Internal.GetTestIdForFastStart
{
    public class GetTestIdForFastStartQueryHandler : IRequestHandler<GetTestIdForFastStartQuery, TestDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly DateTime _timeRangeBeforeStart;
        private readonly DateTime _timeRangeAfterStart;
        private readonly IMapper _mapper;
        private readonly int MinutesBeforeStart = 15;

        public GetTestIdForFastStartQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _timeRangeBeforeStart = DateTime.Now.AddMinutes(MinutesBeforeStart);
            _timeRangeAfterStart = DateTime.Now.AddMinutes(-1);
        }

        public async Task<TestDataDto> Handle(GetTestIdForFastStartQuery request, CancellationToken cancellationToken)
        {
            var test = _appDbContext.Tests
                .Include(x => x.UserProfile)
                .Include(x => x.TestTemplate.Settings)
                .Where(test => test.ProgrammedTime <= _timeRangeBeforeStart &&
                               test.ProgrammedTime >= _timeRangeAfterStart &&
                               test.StartTime == null)
                .FirstOrDefault(x => x.UserProfile.CoreUserId == request.CoreUserProfileId);

            return test == null ? new TestDataDto() : _mapper.Map<TestDataDto>(test);
        }
    }
}
