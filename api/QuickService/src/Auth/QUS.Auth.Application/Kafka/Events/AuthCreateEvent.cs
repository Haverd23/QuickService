using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Kafka.Events
{
    public class AuthCreateEvent
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid AuthId { get; set; }
    }
}
