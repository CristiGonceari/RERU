using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateSettings
{
    public class GetTestTemplateSettingsQueryHandler : IRequestHandler<GetTestTemplateSettingsQuery, TestTemplateSettingsDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTemplateSettingsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<TestTemplateSettingsDto> Handle(GetTestTemplateSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _appDbContext.testTemplateSettings.FirstOrDefaultAsync(x => x.TestTemplateId == request.TestTemplateId);

            return settings == null ? new TestTemplateSettingsDto() : _mapper.Map<TestTemplateSettingsDto>(settings);
        }
    }
}
