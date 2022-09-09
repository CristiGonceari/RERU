using CODWER.RERU.Evaluation.DataTransferObjects.FileTestAnswers;
using CVU.ERP.StorageService;
using MediatR;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.FileTestAnswers.GetFileTestAnswer
{
    public class GetFileTestAnswerQueryHandler : IRequestHandler<GetFileTestAnswerQuery, GetTestFileDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageFileService;

        public GetFileTestAnswerQueryHandler(AppDbContext appDbContext, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
        }

        public async Task<GetTestFileDto> Handle(GetFileTestAnswerQuery request, CancellationToken cancellationToken)
        {
            var testQuestion = _appDbContext.TestQuestions.First(x => x.TestId == request.TestId && x.Index == request.QuestionIndex);

            var item = _appDbContext.FileTestAnswers.OrderBy(x => x.CreateDate).LastOrDefault(x => x.TestQuestionId == testQuestion.Id);

            if (item != null && item.FileId != null)
            {
                return new GetTestFileDto
                {
                    FileName = await _storageFileService.GetFileName(item.FileId),
                    FileId = item.FileId
                };
            }

            return new GetTestFileDto();
        }
    }
}
