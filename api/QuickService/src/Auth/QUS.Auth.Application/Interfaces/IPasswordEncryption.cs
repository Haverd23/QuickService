using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Interfaces
{
    public interface IPasswordEncryption

    {
        string PasswordHash(string password);
        bool PasswordCheck(string password, string storedHash);
    }
}
