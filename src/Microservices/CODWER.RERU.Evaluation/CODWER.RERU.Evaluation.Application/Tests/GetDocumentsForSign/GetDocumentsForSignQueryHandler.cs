using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.DocumentForSign;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.GetDocumentsForSign
{
    public class GetDocumentsForSignQueryHandler : IRequestHandler<GetDocumentsForSignQuery, List<DocumentForSignDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetDocumentsForSignQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<DocumentForSignDto>> Handle(GetDocumentsForSignQuery request, CancellationToken cancellationToken)
        {
            var documentsForSign = await _appDbContext.DocumentsForSign
                    .Where(x => x.TestId == request.TestId)
                    .Select(dfs => new DocumentsForSign
                    {
                        Id = dfs.Id,
                        FileName = dfs.FileName,
                        MediaFileId = dfs.MediaFileId,

                        SignedDocuments = dfs.SignedDocuments
                            .Where(sd => sd.Status == SignRequestStatusEnum.Success)
                            .Select(sd=>new SignedDocument
                            {
                                UserProfileId = sd.UserProfileId,
                                UserProfile = new UserProfile
                                {
                                    FirstName = sd.UserProfile.FirstName,
                                    LastName = sd.UserProfile.LastName,
                                    FatherName = sd.UserProfile.FatherName
                                },
                                
                                SignRequestId = sd.SignRequestId,
                                Status = sd.Status
                            }).ToList()
                    })
                    .ToListAsync();

            var documentForSignDto = _mapper.Map<List<DocumentForSignDto>>(documentsForSign);

            return documentForSignDto;
        }
    }
}
