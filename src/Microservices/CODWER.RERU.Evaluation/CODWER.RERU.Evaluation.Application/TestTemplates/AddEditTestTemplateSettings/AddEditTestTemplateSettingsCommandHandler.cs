﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddEditTestTemplateSettings
{
    public class AddEditTestTemplateSettingsCommandHandler : IRequestHandler<AddEditTestTemplateSettingsCommand, Unit>
    {
        private readonly AppDbContext _appDbContex;
        private readonly IMapper _mapper;

        public AddEditTestTemplateSettingsCommandHandler(AppDbContext appDbContex, IMapper mapper)
        {
            _appDbContex = appDbContex;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddEditTestTemplateSettingsCommand request, CancellationToken cancellationToken)
        {
            var existingSettings = await _appDbContex.TestTypeSettings.FirstOrDefaultAsync(x => x.TestTemplateId == request.Data.TestTemplateId);

            if (existingSettings == null)
            {
                var settingsToAdd = _mapper.Map<TestTypeSettings>(request.Data);
                _appDbContex.TestTypeSettings.Add(settingsToAdd);
            }
            else
            {
                _mapper.Map(request.Data, existingSettings);
            }

            await _appDbContex.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
