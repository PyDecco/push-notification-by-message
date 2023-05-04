using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Test.Wrappers
{
    public interface IBCryptWrapper
    {
        string HashPassword(string password, string salt);
        bool Verify(string password, string hashedPassword);
    }
}
