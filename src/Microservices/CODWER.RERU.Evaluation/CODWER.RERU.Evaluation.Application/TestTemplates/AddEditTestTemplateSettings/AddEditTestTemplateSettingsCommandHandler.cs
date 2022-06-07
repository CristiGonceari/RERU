using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddEditTestTemplateSettings
{
    public class AddEditTestTemplateSettingsCommandHandler : IRequestHandler<AddEditTestTemplateSettingsCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddEditTestTemplateSettingsCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddEditTestTemplateSettingsCommand request, CancellationToken cancellationToken)
        {
            var existingSettings = await _appDbContext.TestTemplateSettings.FirstOrDefaultAsync(x => x.TestTemplateId == request.Data.TestTemplateId);

            if (existingSettings == null)
            {
                var settingsToAdd = _mapper.Map<TestTemplateSettings>(request.Data);
                _appDbContext.TestTemplateSettings.Add(settingsToAdd);
            }
            else
            {
                _mapper.Map(request.Data, existingSettings);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
