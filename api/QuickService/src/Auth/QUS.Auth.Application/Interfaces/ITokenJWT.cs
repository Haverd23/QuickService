using QUS.Auth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Interfaces
{
    public interface ITokenJWT
    {
        Task<string> TokenGenerate(User user);

    }
}
