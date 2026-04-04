using QUS.Notification.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Notification.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage emailMessage);
    }
}
