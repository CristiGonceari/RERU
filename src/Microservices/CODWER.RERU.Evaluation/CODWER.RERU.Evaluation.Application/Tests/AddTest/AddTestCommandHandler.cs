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
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;

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
                var eventtestTemplate = await _appDbContext.EventtestTemplates.FirstOrDefaultAsync(x => x.EventId == request.Data.EventId.Value && x.TestTemplateId == request.Data.TestTemplateId);

                if (eventtestTemplate?.MaxAttempts != null)
                {
                    var attempts = _appDbContext.Tests
                        .Where(x => x.UserProfileId == request.Data.UserProfileId 
                                        && x.EventId == request.Data.EventId.Value 
                                        && x.TestTemplateId == request.Data.TestTemplateId).Count();

                    if (attempts >= eventtestTemplate?.MaxAttempts)
                    {
                        newTest.TestPassStatus = TestPassStatusEnum.Forbidden;
                    }
                }
            }

            _appDbContext.Tests.Add(newTest);
            await _appDbContext.SaveChangesAsync();

            await _internalNotificationService.AddNotification(newTest.UserProfileId, NotificationMessages.YouHaveNewProgrammedTest);
            
            if(newTest.EvaluatorId != null) 
            {
                await _internalNotificationService.AddNotification((int)newTest.EvaluatorId, NotificationMessages.YouWereInvitedToTestAsEvaluator);
            }

            var user = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == newTest.UserProfileId);
            var testTemplate = await _appDbContext.TestTemplates.FirstOrDefaultAsync(x => x.Id == newTest.TestTemplateId);

            await _loggerService.Log(LogData.AsEvaluation($"User {user.GetFullName()} was assigned to test with {testTemplate.Name} test template at {newTest.ProgrammedTime:dd/MM/yyyy HH:mm}"));

            return newTest.Id;
        }
    }
}
