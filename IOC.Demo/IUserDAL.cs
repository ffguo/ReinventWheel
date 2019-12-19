using System;
using System.Collections.Generic;
using System.Text;

namespace IOC.Demo
{
    public interface IUserDAL
    {
        User Get(int id);
    }
}
