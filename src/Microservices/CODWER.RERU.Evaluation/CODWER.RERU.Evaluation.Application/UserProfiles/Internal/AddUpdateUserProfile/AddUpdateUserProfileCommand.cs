using CVU.ERP.Module.Application.Models.Internal;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.Internal.AddUpdateUserProfile
{
    public class AddUpdateUserProfileCommand : IRequest<Unit>
    {
        public BaseUserProfile Data { get; set; }

        public AddUpdateUserProfileCommand(BaseUserProfile data)
        {
            Data = data;
        }
    }
}
