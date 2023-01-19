using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.EmployeeFunctions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions.GetEmployeeFunction
{
    public class GetEmployeeFunctionQueryHandler : IRequestHandler<GetEmployeeFunctionQuery, EmployeeFunctionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetEmployeeFunctionQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<EmployeeFunctionDto> Handle(GetEmployeeFunctionQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.EmployeeFunctions
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<EmployeeFunctionDto>(item);

            return mappedItem;
        }
    }
}
