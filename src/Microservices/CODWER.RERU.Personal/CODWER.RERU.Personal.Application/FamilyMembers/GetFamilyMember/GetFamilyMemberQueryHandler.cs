using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.FamilyMembers.GetFamilyMember
{
    public class GetFamilyMemberQueryHandler : IRequestHandler<GetFamilyMemberQuery, FamilyMemberDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetFamilyMemberQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<FamilyMemberDto> Handle(GetFamilyMemberQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.FamilyMembers
                .Include(x => x.Relation)
                .FirstAsync(d => d.Id == request.Id);

            var mappedItem = _mapper.Map<FamilyMemberDto>(item);

            return mappedItem;
        }
    }
}
