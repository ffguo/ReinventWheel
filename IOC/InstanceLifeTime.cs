using System;
using System.Collections.Generic;
using System.Text;

namespace IOC
{
    public enum InstanceLifeTime
    {
        InstancePer,
        InstancePerThread,
        SingleInstance,
    }
}
