using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.InternalTest;
using CVU.ERP.ServiceProvider;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common;

namespace CODWER.RERU.Evaluation.Application.Tests.Internal.GetTestIdForFastStart
{
    public class GetTestIdForFastStartQueryHandler : IRequestHandler<GetTestIdForFastStartQuery, List<GetTestForFastStartDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentApplicationUserProvider _currentApplicationUserProvider;
        private readonly IDateTime _dateTime;
        private readonly DateTime _timeRangeBeforeStart;
        private readonly DateTime _timeRangeAfterStart;
        private readonly IMapper _mapper;

        public GetTestIdForFastStartQueryHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            ICurrentApplicationUserProvider currentApplicationUserProvider, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _currentApplicationUserProvider = currentApplicationUserProvider;
            _dateTime = dateTime;
            _timeRangeBeforeStart = _dateTime.Now.AddMinutes(15);
            _timeRangeAfterStart = _dateTime.Now.AddMinutes(-1);
        }

        public async Task<List<GetTestForFastStartDto>> Handle(GetTestIdForFastStartQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentApplicationUserProvider.Get();

            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate.Settings)
                .Include(x => x.TestTemplate)
                .Include(x => x.Event)
                .Where(test => test.UserProfileId == int.Parse(currentUser.Id) &&
                               (test.Event == null ?
                                        test.ProgrammedTime <= _timeRangeBeforeStart && test.ProgrammedTime >= _timeRangeAfterStart :
                                        test.Event.FromDate <= _dateTime.Now && test.Event.TillDate >= _dateTime.Now) &&
                            test.TestTemplate.Mode == TestTemplateModeEnum.Test &&
                                                        (test.TestStatus == TestStatusEnum.Programmed || 
                                                         test.TestStatus == TestStatusEnum.AlowedToStart))
                .Select(u => _mapper.Map<GetTestForFastStartDto>(u))
                .ToListAsync();

            return test;
        }
    }
}
