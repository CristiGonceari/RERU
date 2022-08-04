using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Personal.DataTransferObjects.Contracts;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.DataTransferObjects.Instructions;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorCim.GetContractorCim
{
    public class GetContractorCimQueryHandler : IRequestHandler<GetContractorCimQuery, IndividualContractDetails>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public GetContractorCimQueryHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<IndividualContractDetails> Handle(GetContractorCimQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var contract = await _appDbContext.Contracts.FirstOrDefaultAsync(x => x.ContractorId == contractorId);
            var instructions = _appDbContext.Instructions.Where(x => x.ContractorId == contractorId).AsEnumerable();

            var contractDto = _mapper.Map<IndividualContractDetails>(contract);
            contractDto.Instructions = instructions.Select(_mapper.Map<AddEditInstructionDto>).ToList();

            return contractDto;
        }
    }
}
