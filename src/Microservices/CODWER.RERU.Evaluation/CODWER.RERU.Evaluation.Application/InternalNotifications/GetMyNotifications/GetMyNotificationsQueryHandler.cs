using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Notifications;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.InternalNotifications.GetMyNotifications
{
    public class GetMyNotificationsQueryHandler : IRequestHandler<GetMyNotificationsQuery, List<NotificationDto>>
    {
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public GetMyNotificationsQueryHandler(IInternalNotificationService internalNotificationService, IMediator mediator, IMapper mapper, IUserProfileService userProfileService)
        {
            _internalNotificationService = internalNotificationService;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<List<NotificationDto>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
        {
            var currentUserProfileId = await _userProfileService.GetCurrentUserId();
            var myNotifications = await _internalNotificationService.GetMyNotifications(currentUserProfileId);

            return _mapper.Map<List<NotificationDto>>(myNotifications);
        }
    }
}
