using QUS.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Core.Data
{
    public interface IRepository <T> : IDisposable where T : IAggregateRoot
    {
        IUnityOfWork UnitOfWork { get; }
    }
}
