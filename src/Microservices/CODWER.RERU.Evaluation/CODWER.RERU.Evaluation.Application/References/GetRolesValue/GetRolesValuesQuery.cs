﻿using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.References.GetRolesValue
{
    public class GetRolesValuesQuery : IRequest<List<SelectItem>>
    {
    }
}
