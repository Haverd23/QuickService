using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Interfaces
{
    public interface ILogin
    {
        Task<string> Authenticate(string email, string password);
    }
}
