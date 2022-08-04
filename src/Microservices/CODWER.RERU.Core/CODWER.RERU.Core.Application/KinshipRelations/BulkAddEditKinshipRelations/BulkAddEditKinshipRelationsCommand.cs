using CODWER.RERU.Core.DataTransferObjects.KinshipRelation;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.KinshipRelations.BulkAddEditKinshipRelations
{
    public class BulkAddEditKinshipRelationsCommand : IRequest<Unit>
    {
        public BulkAddEditKinshipRelationsCommand(List<KinshipRelationDto> list)
        {
            Data = list;
        }

        public List<KinshipRelationDto> Data { get; set; }
    }
}
