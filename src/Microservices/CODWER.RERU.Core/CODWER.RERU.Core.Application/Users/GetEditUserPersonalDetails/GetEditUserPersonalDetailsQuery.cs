using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.GetEditUserPersonalDetails 
{
    public class GetEditUserPersonalDetailsQuery : IRequest<EditUserPersonalDetailsDto>
    {
        public GetEditUserPersonalDetailsQuery (int id) {
            Id = id;
        }

        public int Id { set; get; }
    }
}