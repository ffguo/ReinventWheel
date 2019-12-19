using System;
using System.Collections.Generic;
using System.Text;

namespace IOC.Demo
{
    public class LoginDAL : ILoginDAL
    {
        public bool IsLogin()
        {
            return true;
        }
    }
}
