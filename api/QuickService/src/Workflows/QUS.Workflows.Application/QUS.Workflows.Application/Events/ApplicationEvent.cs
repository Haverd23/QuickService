using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Workflows.Application.Events
{
    public abstract class ApplicationEvent : IApplicationEvent
    {
        public Guid EventId { get; }
        public DateTime OccurredOn { get; }
        public string EventType => GetType().Name;

        protected ApplicationEvent()
        {
            EventId = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }
    }
}
