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
        private readonly IAssignRolesToArticle _assignRolesToArticle;

        public AddArticleCommandHandler(AppDbContext appDbContext, IMapper mapper, IStorageFileService storageFileService, IAssignRolesToArticle assignRolesToArticle)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageFileService = storageFileService;
            _assignRolesToArticle = assignRolesToArticle;
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

            await _assignRolesToArticle.AssignRolesToArticle(request.Roles, articleToCreate.Id);

            return articleToCreate.Id;
        }
    }
}
