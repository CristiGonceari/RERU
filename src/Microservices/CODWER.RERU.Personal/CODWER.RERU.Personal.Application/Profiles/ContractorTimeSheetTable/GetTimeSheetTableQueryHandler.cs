using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorTimeSheetTable
{
    public class GetTimeSheetTableQueryHandler : IRequestHandler<GetTimeSheetTableQuery, ContractorProfileTimeSheetTableDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ITimeSheetTableService _timeSheetTableService;
        private readonly IUserProfileService _userProfileService;

        public GetTimeSheetTableQueryHandler(AppDbContext appDbContext,
            IMapper mapper,
            ITimeSheetTableService timeSheetTableService, 
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _timeSheetTableService = timeSheetTableService;
            _userProfileService = userProfileService;
        }

        public async Task<ContractorProfileTimeSheetTableDto> Handle(GetTimeSheetTableQuery request, CancellationToken cancellationToken)
        {
            var contractor = await _userProfileService.GetCurrentContractor();

            var content = _appDbContext.TimeSheetTables
                .Where(x => x.ContractorId == contractor.Id)
                .Where(x=> x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date)
                .ToList();

            var contractorTimeSheetDto = _mapper.Map<ContractorProfileTimeSheetTableDto>(contractor);
            contractorTimeSheetDto.Content = content.Select(_mapper.Map<TimeSheetTableDto>).ToList();

            for (var i = request.FromDate.Date; i <= request.ToDate.Date; i = i.AddDays(1))
            {
                if (contractorTimeSheetDto.Content.All(x => x.Date.Date != i))
                {
                    contractorTimeSheetDto.Content.Add(new TimeSheetTableDto
                    {
                        ContractorId = contractor.Id,
                        Date = i
                    });
                }
            }

            contractorTimeSheetDto.Content = contractorTimeSheetDto.Content.OrderBy(x => x.Date).ToList();
            contractorTimeSheetDto.WorkingDays = await _timeSheetTableService.GetWorkingDays(request.FromDate, request.ToDate);
            contractorTimeSheetDto.WorkedHours = await _timeSheetTableService.GetWorkedHoursByTimeSheet(content);

            contractorTimeSheetDto.FreeHours = await _timeSheetTableService
                .GetFreeHoursForContractor(contractor.Id, contractorTimeSheetDto.WorkedHours, request.FromDate, request.ToDate, contractorTimeSheetDto.WorkingDays);

            return contractorTimeSheetDto;
        }
    }
}
