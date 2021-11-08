using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.AddEditTestTypeSettings
{
    public class AddEditTestTypeSettingsCommandHandler : IRequestHandler<AddEditTestTypeSettingsCommand, Unit>
    {
        private readonly AppDbContext _appDbContex;
        private readonly IMapper _mapper;

        public AddEditTestTypeSettingsCommandHandler(AppDbContext appDbContex, IMapper mapper)
        {
            _appDbContex = appDbContex;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddEditTestTypeSettingsCommand request, CancellationToken cancellationToken)
        {
            var existingSettings = await _appDbContex.TestTypeSettings.FirstOrDefaultAsync(x => x.TestTypeId == request.Input.TestTypeId);

            if (existingSettings == null)
            {
                var settingsToAdd = _mapper.Map<TestTypeSettings>(request.Input);
                _appDbContex.TestTypeSettings.Add(settingsToAdd);
            }
            else
            {
                _mapper.Map(request.Input, existingSettings);
            }

            await _appDbContex.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
