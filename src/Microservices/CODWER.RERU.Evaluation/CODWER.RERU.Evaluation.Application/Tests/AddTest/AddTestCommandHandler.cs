using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities.StaticExtensions;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.LoggerServices;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTest
{
     public class AddTestCommandHandler : IRequestHandler<AddTestCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly ILoggerService<AddTestCommandHandler> _loggerService;

        public AddTestCommandHandler(AppDbContext appDbContext, IMapper mapper, 
            IInternalNotificationService internalNotificationService,
            ILoggerService<AddTestCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _internalNotificationService = internalNotificationService;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddTestCommand request, CancellationToken cancellationToken)
        {
            var newTest = _mapper.Map<Test>(request.Data);

            newTest.TestStatus = TestStatusEnum.Programmed;

            if (request.Data.EventId.HasValue)
            {
                var eventTestType = await _appDbContext.EventTestTypes.FirstOrDefaultAsync(x => x.EventId == request.Data.EventId.Value && x.TestTypeId == request.Data.TestTypeId);

                if (eventTestType?.MaxAttempts != null)
                {
                    var attempts = _appDbContext.Tests
                        .Where(x => x.UserProfileId == request.Data.UserProfileId 
                                        && x.EventId == request.Data.EventId.Value 
                                        && x.TestTypeId == request.Data.TestTypeId).Count();

                    if (attempts >= eventTestType?.MaxAttempts)
                    {
                        newTest.TestPassStatus = TestPassStatusEnum.Forbidden;
                    }
                }
            }

            _appDbContext.Tests.Add(newTest);
            await _appDbContext.SaveChangesAsync();

            await _internalNotificationService.AddNotification(newTest.UserProfileId, NotificationMessages.YouHaveNewProgrammedTest);

            var user = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == newTest.UserProfileId);
            var testType = await _appDbContext.TestTypes.FirstOrDefaultAsync(x => x.Id == newTest.TestTypeId);

            await _loggerService.Log(new LogData($"User {user.GetFullName()} was assigned to test with {testType.Name} test template at {newTest.ProgrammedTime.ToUniversalTime()}").AsEvaluation());

            return newTest.Id;
        }
    }
}
