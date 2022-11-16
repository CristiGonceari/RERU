﻿using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Plans.EditPlan
{
    public class EditPlanCommandHandler : IRequestHandler<EditPlanCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<EditPlanCommandHandler> _loggerService;

        public EditPlanCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<EditPlanCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(EditPlanCommand request, CancellationToken cancellationToken)
        {
            var planToEdit = await _appDbContext.Plans.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, planToEdit);
            await _appDbContext.SaveChangesAsync();
            await LogAction(planToEdit);

            return planToEdit.Id;
        }

        private async Task LogAction(Plan item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Planul {item.Name} a fost actualizat în sistem cu descrierea {item.Description}", item));
        }
    }
}
