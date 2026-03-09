using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Core.IntegrationEvent
{
    public interface IApplicationEvent
    {
        public DateTime OccurredOn { get; }
        public Guid EventId { get; }
        public string EventType { get; }
    }
}
