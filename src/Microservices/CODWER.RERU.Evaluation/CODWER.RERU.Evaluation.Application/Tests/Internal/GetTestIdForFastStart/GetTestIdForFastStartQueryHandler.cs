using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.TestDatas;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.ServiceProvider;

namespace CODWER.RERU.Evaluation.Application.Tests.Internal.GetTestIdForFastStart
{
    public class GetTestIdForFastStartQueryHandler : IRequestHandler<GetTestIdForFastStartQuery, TestDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentApplicationUserProvider _currentApplicationUserProvider;
        private readonly DateTime _timeRangeBeforeStart;
        private readonly DateTime _timeRangeAfterStart;
        private readonly IMapper _mapper;

        public GetTestIdForFastStartQueryHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            ICurrentApplicationUserProvider currentApplicationUserProvider)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _currentApplicationUserProvider = currentApplicationUserProvider;
            _timeRangeBeforeStart = DateTime.Now.AddMinutes(15);
            _timeRangeAfterStart = DateTime.Now.AddMinutes(-1);
        }

        public async Task<TestDataDto> Handle(GetTestIdForFastStartQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentApplicationUserProvider.Get();

            //var test = _appDbContext.Tests
            //    .Include(x => x.TestTemplate.Settings)
            //    .Where(test => test.ProgrammedTime <= _timeRangeBeforeStart &&
            //                   test.ProgrammedTime >= _timeRangeAfterStart &&
            //                   (test.TestStatus == TestStatusEnum.Programmed || test.TestStatus == TestStatusEnum.AlowedToStart))
            //    .FirstOrDefault(x => x.UserProfileId == int.Parse(currentUser.Id));

            var test = _appDbContext.Tests
                .Include(x => x.TestTemplate.Settings)
                .Include(x => x.TestTemplate)
                .Where(test => test.UserProfileId == int.Parse(currentUser.Id) &&
                            test.TestTemplate.Mode == TestTemplateModeEnum.Test &&
                            (test.TestStatus == TestStatusEnum.Programmed || test.TestStatus == TestStatusEnum.AlowedToStart))
                .FirstOrDefault(test => test.Event == null
                    ? test.ProgrammedTime <= _timeRangeBeforeStart && test.ProgrammedTime >= _timeRangeAfterStart
                    : test.Event.FromDate <= DateTime.Now && test.Event.TillDate >= DateTime.Now);

            return test == null ? new TestDataDto() : _mapper.Map<TestDataDto>(test);
        }
    }
}
