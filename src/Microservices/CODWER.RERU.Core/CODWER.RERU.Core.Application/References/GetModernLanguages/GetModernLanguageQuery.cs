using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.References.GetModernLanguages
{
    public class GetModernLanguageQuery : IRequest<List<SelectValue>>
    {
    }
}
