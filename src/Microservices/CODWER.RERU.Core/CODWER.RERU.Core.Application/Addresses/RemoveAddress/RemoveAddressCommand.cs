using MediatR;

namespace CODWER.RERU.Core.Application.Addresses.RemoveAddress
{
    public class RemoveAddressCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
