using QUS.Core.IntegrationEvent;
using QUS.Notification.Application.DTOs;
using QUS.Notification.Application.Kafka.Events;
using QUS.Notification.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Notification.Application.Kafka.EventsHandlers
{
    public class UserCreatedEventHandler : IIntegrationEventHandler<UserCreatedEvent>
    {
        private readonly NotificationService _notificationService;

        public UserCreatedEventHandler(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task HandleAsync(UserCreatedEvent @event)
        {
            var emailMessage = new EmailMessage
            {
                To = @event.Email,
                Subject = "Welcome to QUS!",
                Body = $"Hello {@event.Name},\n\nThank you for registering at QUS. We're excited to have you on board!"
            };
            await _notificationService.SendEmailAsync(emailMessage);
        }
    }
}
