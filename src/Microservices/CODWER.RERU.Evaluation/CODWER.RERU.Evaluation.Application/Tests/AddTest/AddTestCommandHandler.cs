using AutoMapper;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTest
{
    public class AddTestCommandHandler : IRequestHandler<AddTestCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public AddTestCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            INotificationService notificationService)
        {
            _appDbContext = appDbContext.NewInstance();
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<int> Handle(AddTestCommand request, CancellationToken cancellationToken)
        {
            var newTest = _mapper.Map<Test>(request.Data);

            var eventData = _appDbContext.Events.FirstOrDefault(e => e.Id == newTest.EventId);

            if (eventData != null)
            {
                newTest.ProgrammedTime = eventData.FromDate;
                newTest.EndProgrammedTime = eventData.TillDate;
            }

            newTest.TestStatus = TestStatusEnum.Programmed;

            if (request.Data.EventId.HasValue)
            {
                var eventTestTemplate = await _appDbContext.EventTestTemplates.FirstOrDefaultAsync(x => x.EventId == request.Data.EventId.Value && x.TestTemplateId == request.Data.TestTemplateId);

                if (eventTestTemplate?.MaxAttempts != null)
                {
                    var attempts = _appDbContext.Tests.Count(x => x.UserProfileId == request.Data.UserProfileId 
                                                                  && x.EventId == request.Data.EventId.Value 
                                                                  && x.TestTemplateId == request.Data.TestTemplateId);

                    if (attempts >= eventTestTemplate?.MaxAttempts)
                    {
                        newTest.TestPassStatus = TestPassStatusEnum.Forbidden;
                    }
                }   
            }

            _appDbContext.Tests.Add(newTest);
            await _appDbContext.SaveChangesAsync();

            return newTest.Id;
        }
    }
}
