using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.Application.Services;

namespace CODWER.RERU.Evaluation.Application.Options.EditOption
{
    public class EditOptionsCommandHandler : IRequestHandler<EditOptionsCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IStorageFileService _storageFileService;

        public EditOptionsCommandHandler(AppDbContext appDbContext, IMapper mapper, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageFileService = storageFileService;
        }

        public async Task<Unit> Handle(EditOptionsCommand request, CancellationToken cancellationToken)
        {
            var editOptions = await _appDbContext.Options.FirstOrDefaultAsync(x => x.Id == request.Data.Id);


            if (request.Data.FileDto != null)
            {
                var addFile = await _storageFileService.AddFile(request.Data.FileDto);

                editOptions.MediaFileId = addFile;
            }
            else 
            {
                editOptions.MediaFileId = request.Data.MediaFileId;
            }


            editOptions.QuestionUnitId = request.Data.QuestionUnitId;
            editOptions.IsCorrect = request.Data.IsCorrect;
            editOptions.Answer = request.Data.Answer;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
