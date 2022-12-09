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
using CVU.ERP.Common;
using CVU.ERP.ServiceProvider;

namespace CODWER.RERU.Core.Application.Internal.GetTestIdFastStart
{
    public class GetTestIdFastStartQueryHandler : IRequestHandler<GetTestIdFastStartQuery, TestDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentApplicationUserProvider _currentApplicationUserProvider;
        private readonly DateTime _timeRangeBeforeStart;
        private readonly DateTime _timeRangeAfterStart;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;

        public GetTestIdFastStartQueryHandler(AppDbContext appDbContext, 
            ICurrentApplicationUserProvider currentApplicationUserProvider, 
            IMapper mapper, IDateTime dateTime) 
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _dateTime = dateTime;
            _currentApplicationUserProvider = currentApplicationUserProvider;
            _timeRangeBeforeStart = _dateTime.Now.AddMinutes(15);
            _timeRangeAfterStart = _dateTime.Now.AddMinutes(-1);
        }

        public async Task<TestDataDto> Handle(GetTestIdFastStartQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentApplicationUserProvider.Get();

            //var test = _appDbContext.Tests
            //    .Include(x => x.TestTemplate.Settings)
            //    .Where(test => test.ProgrammedTime <= _timeRangeBeforeStart &&
            //        test.ProgrammedTime >= _timeRangeAfterStart &&
            //        (test.TestStatus == TestStatusEnum.Programmed ||
            //         test.TestStatus == TestStatusEnum.AlowedToStart))
            //    .FirstOrDefault(x => x.UserProfileId == int.Parse(currentUser.Id));

            var test = _appDbContext.Tests
                .Include(x => x.TestTemplate.Settings)
                .Where(test => test.UserProfileId == int.Parse(currentUser.Id) &&
                 (test.TestStatus == TestStatusEnum.Programmed || test.TestStatus == TestStatusEnum.AlowedToStart))
                .FirstOrDefault(test => test.Event == null
                    ? test.ProgrammedTime <= _timeRangeBeforeStart && test.ProgrammedTime >= _timeRangeAfterStart
                    : test.Event.FromDate <= _dateTime.Now && test.Event.TillDate >= _dateTime.Now);

            return test == null ? new TestDataDto() : _mapper.Map<TestDataDto>(test);
        }
    }
}
