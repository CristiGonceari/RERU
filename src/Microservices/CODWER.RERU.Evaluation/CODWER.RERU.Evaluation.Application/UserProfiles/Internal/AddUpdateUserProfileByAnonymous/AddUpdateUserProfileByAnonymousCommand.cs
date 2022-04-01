using CVU.ERP.Module.Application.Models.Internal;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.Internal.AddUpdateUserProfileByAnonymous
{
    public class AddUpdateUserProfileByAnonymousCommand : IRequest<Unit>
    {
        public BaseUserProfile Data { get; set; }

        public AddUpdateUserProfileByAnonymousCommand(BaseUserProfile data)
        {
            Data = data;
        }
    }
}
