﻿using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionsSelectValues
{
    public class GetPositionsSelectValuesQuery : IRequest<List<SelectItem>>
    {
        public int? Id { get; set; }
    }
}
