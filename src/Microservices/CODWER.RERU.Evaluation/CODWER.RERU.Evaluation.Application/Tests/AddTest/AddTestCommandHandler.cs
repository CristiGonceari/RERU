using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTest
{
     public class AddTestCommandHandler : IRequestHandler<AddTestCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddTestCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddTestCommand request, CancellationToken cancellationToken)
        {
            var newTest = _mapper.Map<Test>(request.Data);

            var eventDatas = _appDbContext.Events.FirstOrDefault(e => e.Id == newTest.EventId);

            if (eventDatas != null)
            {
                newTest.ProgrammedTime = eventDatas.FromDate;
                newTest.EndProgrammedTime = eventDatas.TillDate;
            }

            newTest.TestStatus = TestStatusEnum.Programmed;

            if (request.Data.EventId.HasValue)
            {
                var eventtestTemplate = await _appDbContext.EventTestTemplates.FirstOrDefaultAsync(x => x.EventId == request.Data.EventId.Value && x.TestTemplateId == request.Data.TestTemplateId);

                if (eventtestTemplate?.MaxAttempts != null)
                {
                    var attempts = _appDbContext.Tests.Count(x => x.UserProfileId == request.Data.UserProfileId 
                                                                  && x.EventId == request.Data.EventId.Value 
                                                                  && x.TestTemplateId == request.Data.TestTemplateId);

                    if (attempts >= eventtestTemplate?.MaxAttempts)
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
