using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Core.DomainObjects
{
    public interface IDomainEvent 
    {
        public DateTime OccurredOn { get; }
    }
}
