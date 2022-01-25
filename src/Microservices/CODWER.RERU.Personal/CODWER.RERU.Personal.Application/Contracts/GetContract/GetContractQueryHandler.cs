using System.Linq;
using System.Threading;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contracts;
using CODWER.RERU.Personal.DataTransferObjects.Instructions;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contracts.GetContract
{
    public class GetContractQueryHandler : IRequestHandler<GetContractQuery, IndividualContractDetails>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IndividualContractDetails> Handle(GetContractQuery request, CancellationToken cancellationToken)
        {
            var contract = await _appDbContext.Contracts.FirstOrDefaultAsync(x => x.ContractorId == request.ContractorId);
            var instructions = _appDbContext.Instructions.Where(x => x.ContractorId == request.ContractorId).AsEnumerable();

            var contractDto = _mapper.Map<IndividualContractDetails>(contract);
            contractDto.Instructions = instructions.Select(_mapper.Map<AddEditInstructionDto>).ToList();

            return contractDto;
        }
    }
}
