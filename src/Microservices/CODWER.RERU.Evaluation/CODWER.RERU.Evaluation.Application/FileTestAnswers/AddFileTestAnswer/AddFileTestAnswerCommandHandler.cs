using CVU.ERP.StorageService;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.FileTestAnswers.AddFileTestAnswer
{
    public class AddFileTestAnswerCommandHandler : IRequestHandler<AddFileTestAnswerCommand, string>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageService;

        public AddFileTestAnswerCommandHandler(AppDbContext appDbContext, IStorageFileService storageService)
        {
            _appDbContext = appDbContext;
            _storageService = storageService;
        }

        public async Task<string> Handle(AddFileTestAnswerCommand request, CancellationToken cancellationToken)
        {
            if(request.FileDto.File == null) return null;
            var fileId = await _storageService.AddFile(request.FileDto);

            var testQuestion = _appDbContext.TestQuestions.First(x => x.Index == request.QuestionIndex && x.TestId == request.TestId);

            var fileTestAnswer = new FileTestAnswer
            {
                TestQuestionId = testQuestion.Id,
                FileId = fileId
            };

            await _appDbContext.FileTestAnswers.AddAsync(fileTestAnswer);
            await _appDbContext.SaveChangesAsync();

            return fileId;
        }
    }
}
