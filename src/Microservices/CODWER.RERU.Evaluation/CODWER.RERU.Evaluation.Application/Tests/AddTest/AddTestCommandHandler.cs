using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

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

            return newTest.Id;
        }
    }
}
