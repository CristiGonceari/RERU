using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeSettings
{
    public class GetTestTypeSettingsQueryHandler : IRequestHandler<GetTestTypeSettingsQuery, TestTypeSettingsDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTypeSettingsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<TestTypeSettingsDto> Handle(GetTestTypeSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _appDbContext.TestTypeSettings.FirstOrDefaultAsync(x => x.TestTypeId == request.TestTypeId);

            return settings == null ? new TestTypeSettingsDto() : _mapper.Map<TestTypeSettingsDto>(settings);
        }
    }
}
