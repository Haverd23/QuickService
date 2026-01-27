using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Service.Application.Interfaces
{
    public interface IGetUserIdService
    {
        Task<Guid> GetUserId(Guid Id);
    }
}
