using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorEmails
{
   public class GetContractorEmailQueryHandler : IRequestHandler<GetContractorEmailQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractorEmailQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<List<SelectItem>> Handle(GetContractorEmailQuery request, CancellationToken cancellationToken)
        {
            var contractor =  _appDbContext.Contacts
                .Where(c => c.ContractorId == request.Id && c.Type == ContactTypeEnum.Email)
                .OrderBy(c => c.ContractorId)
                .AsQueryable();
                
            return _mapper.Map<List<SelectItem>>(contractor);
        }
    }
}
