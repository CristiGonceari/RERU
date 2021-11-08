using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.References.GetQuestionCategoryValue
{
    public class GetQuestionCategoryValueQueryHandler : IRequestHandler<GetQuestionCategoryValueQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetQuestionCategoryValueQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetQuestionCategoryValueQuery request, CancellationToken cancellationToken)
        {
            var questionCategories = await _appDbContext.QuestionCategories
                .AsQueryable()
                .Select(u => _mapper.Map<SelectItem>(u))
                .ToListAsync();

            return questionCategories;
        }
    }
}
