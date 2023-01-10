using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CVU.ERP.Common;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionsSelectValues
{
    public class GetPositionsSelectValuesQueryHandler : IRequestHandler<GetPositionsSelectValuesQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly IUserProfileService _userProfileService;
        private List<SelectItem> _list = new ();

        public GetPositionsSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper, IDateTime dateTime, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _dateTime = dateTime;
            _userProfileService = userProfileService;
        }

        public async Task<List<SelectItem>> Handle(GetPositionsSelectValuesQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            if (request.Id == null)
            {
                return await GetCandidatePositionsSelectValues(currentUserId);
            }

            var item = await GetSolicitedVacantPosition(request);

            _list.Add(_mapper.Map<SelectItem>(item));

            return _list;
        }

        private async Task<List<SelectItem>> GetCandidatePositionsSelectValues(int currentUserId) =>  
               _appDbContext.CandidatePositions.AsQueryable()
                .Where(x => x.From.Value <= _dateTime.Now &&
                            x.To.Value >= _dateTime.Now &&
                            x.CreateById != currentUserId.ToString() &&
                            x.IsActive)
                .Select(tt => _mapper.Map<SelectItem>(tt))
                .ToList();
        
        private async Task<SolicitedVacantPosition> GetSolicitedVacantPosition(GetPositionsSelectValuesQuery request) =>
              _appDbContext.SolicitedVacantPositions
                .Include(c => c.CandidatePosition)
                .FirstOrDefault(x => x.Id == request.Id);
    }
}
