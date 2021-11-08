using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeByStatus
{
    public class GetTestTypeByStatusQueryHandler : IRequestHandler<GetTestTypeByStatusQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTypeByStatusQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetTestTypeByStatusQuery request, CancellationToken cancellationToken)
        {
            var testTypes = await _appDbContext.TestTypes
                .Include(x => x.Settings)
                .Where(x => x.Status == request.TestTypeStatus && x.Mode == (int)TestTypeModeEnum.Test)
                .Select(x => _mapper.Map<SelectItem>(x))
                .ToListAsync();

            return testTypes;
        }
    }
}
