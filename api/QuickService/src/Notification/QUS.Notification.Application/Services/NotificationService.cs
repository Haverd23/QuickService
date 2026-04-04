using QUS.Notification.Application.DTOs;
using QUS.Notification.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Notification.Application.Services
{
    public class NotificationService
    {
        private readonly IEmailSender _emailSender;

        public NotificationService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            await _emailSender.SendAsync(emailMessage);
        }
    }
}
