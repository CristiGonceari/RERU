using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using CVU.ERP.StorageService;
using CODWER.RERU.Evaluation.DataTransferObjects.Articles;

namespace CODWER.RERU.Evaluation.Application.Articles.AddArticle
{
    public class AddArticleCommandHandler : IRequestHandler<AddArticleCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IStorageFileService _storageFileService;
        private readonly IAssignRoleService _assignRoleService;

        public AddArticleCommandHandler(AppDbContext appDbContext, IMapper mapper, IStorageFileService storageFileService, IAssignRoleService assignRoleService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageFileService = storageFileService;
            _assignRoleService = assignRoleService;
        }

        public async Task<int> Handle(AddArticleCommand request, CancellationToken cancellationToken)
        {
            var storage = await _storageFileService.AddFile(request.FileDto);

            var newArticle = new ArticleEvaluationDto()
            {
                Name = request.Name,
                Content = request.Content,
                MediaFileId = storage,
            };

            var articleToCreate = _mapper.Map<ArticleEvaluation>(newArticle);

            await _appDbContext.EvaluationArticles.AddAsync(articleToCreate);

            await _appDbContext.SaveChangesAsync();

            await _assignRoleService.AssignRolesToArticle(request.Roles, articleToCreate.Id);

            return articleToCreate.Id;
        }
    }
}
