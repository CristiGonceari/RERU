using CODWER.RERU.Evaluation.Application.RequiredDocuments.AddEditRequiredDocument;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class AssignDocumentsToPosition : IAssignDocumentsToPosition
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;

        public AssignDocumentsToPosition(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        public async Task AssignRequiredDocumentsToPosition(List<AssignRequiredDocumentsDto> requiredDocuments, CandidatePosition position)
        {
            var items = _appDbContext.RequiredDocumentPositions.Where(x => x.CandidatePositionId == position.Id).ToList();

            _appDbContext.RequiredDocumentPositions.RemoveRange(items);

            foreach (var document in requiredDocuments)
            {

                if (document.Value != 0)
                {
                    await AddRequiredDocumentPosition(document.Value.Value, position.Id);
                }
                else
                {
                    var result = await AddRequiredDocumentCommand(document);

                    await AddRequiredDocumentPosition(result, position.Id);
                }

            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> AddRequiredDocumentCommand(AssignRequiredDocumentsDto document)
        {
            var requiredDocument = new AddEditRequiredDocumentsCommand
            {
                Data = new AddEditRequiredDocumentsDto
                {
                    Name = document.Display,
                    Mandatory = false
                }
            };

            return await _mediator.Send(requiredDocument);
        }

        public async Task AddRequiredDocumentPosition(int requiredDocumentId, int positionId)
        {
            var rdp = new RequiredDocumentPosition
            {
                RequiredDocumentId = requiredDocumentId,
                CandidatePositionId = positionId
            };

            await _appDbContext.RequiredDocumentPositions.AddAsync(rdp);
        }
       
    }
}
