using System;
using System.Collections.Generic;
using System.Text;

namespace IOC.Demo
{
    public interface IUserServer
    {
        User Get(int id);
    }
}
