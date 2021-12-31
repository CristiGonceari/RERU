using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.UserProfiles.GetCurrentUserProfile;
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
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public GetMyNotificationsQueryHandler(IInternalNotificationService internalMotificationService, IMediator mediator, IMapper mapper)
        {
            _internalNotificationService = internalMotificationService;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<List<NotificationDto>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _mediator.Send(new GetCurrentUserProfileQuery());
            var myNotifications = await _internalNotificationService.GetMyNotifications(myUserProfile.Id);

            return _mapper.Map<List<NotificationDto>>(myNotifications);
        }
    }
}
