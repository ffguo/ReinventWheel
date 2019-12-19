using System;
using System.Collections.Generic;
using System.Text;

namespace IOC
{
    public class InstanceInfo
    {
        public Type Type { get; set; }
        public InstanceLifeTime InstanceLifeTime { get; set; }
        public object Instance { get; set; }
    }
}
