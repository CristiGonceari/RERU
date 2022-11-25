using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;
using CVU.ERP.StorageService;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Options.AddOption
{
    public class AddOptionCommandHandler : IRequestHandler<AddOptionCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IStorageFileService _storageService;

        public AddOptionCommandHandler(AppDbContext appDbContext, IMapper mapper, IStorageFileService storageService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<int> Handle(AddOptionCommand request, CancellationToken cancellationToken)
        {
            var storage = await _storageService.AddFile(request.FileDto);

            var option = new AddEditOptionDto()
            {
                QuestionUnitId = request.QuestionUnitId,
                Answer = request.Answer,
                IsCorrect = request.IsCorrect,
                MediaFileId = string.IsNullOrEmpty(storage) ? null : storage
            };

            var newOption = _mapper.Map<Option>(option);

            await _appDbContext.Options.AddAsync(newOption);
            await _appDbContext.SaveChangesAsync();

            return newOption.Id;
        }
    }
}
