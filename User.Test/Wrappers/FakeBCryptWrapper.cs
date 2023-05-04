using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Test.Wrappers
{
    public class FakeBCryptWrapper
    {
        public string HashPassword(string password, string salt)
        {
            return $"{password}_hashed";
        }

        public bool Verify(string password, string hashedPassword)
        {
            return $"{password}_hashed" == hashedPassword;
        }
    }
}
