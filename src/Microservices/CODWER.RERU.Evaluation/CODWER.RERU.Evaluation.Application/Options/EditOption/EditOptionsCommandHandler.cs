﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Options.EditOption
{
    public class EditOptionsCommandHandler : IRequestHandler<EditOptionsCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public EditOptionsCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditOptionsCommand request, CancellationToken cancellationToken)
        {
            var editOptions = await _appDbContext.Options.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, editOptions);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
