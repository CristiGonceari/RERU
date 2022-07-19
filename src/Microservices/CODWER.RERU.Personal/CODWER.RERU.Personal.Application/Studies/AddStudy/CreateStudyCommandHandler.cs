﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using MediatR;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.Studies.AddStudy
{
    public class CreateStudyCommandHandler : IRequestHandler<CreateStudyCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CreateStudyCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateStudyCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Study>(request.Data);

            await _appDbContext.Studies.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
