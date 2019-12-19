using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace IOC
{
    public class Container
    {
        private Dictionary<string, InstanceInfo> _constains = new Dictionary<string, InstanceInfo>();

        public void Registered<TF, TT>(InstanceLifeTime instanceLifeTime = InstanceLifeTime.InstancePer)
        {
            _constains.TryAdd(typeof(TF).FullName, new InstanceInfo
            {
                Type = typeof(TT),
                InstanceLifeTime = instanceLifeTime
            });
        }

        public T Resolve<T>()
        {
            return (T)ResolveObject(typeof(T));
        }

        private object ResolveObject(Type type)
        {
            // 1.获取实例信息
            _constains.TryGetValue(type.FullName, out InstanceInfo info);

            // 2.根据不同生命周期返回相应实例
            switch (info.InstanceLifeTime)
            {
                case InstanceLifeTime.InstancePer:
                    break;
                case InstanceLifeTime.InstancePerThread:
                    {
                        var data = CallContext.GetData(type.FullName);
                        if (data == null)
                            break;
                        else
                            return data;
                    }
                case InstanceLifeTime.SingleInstance:
                    {
                        if (info.Instance == null)
                            break;
                        else
                            return info.Instance;
                    }
                default:
                    break;
            }

            // 3.创建相应实例
            object instance = CreateInstance(info.Type);

            switch (info.InstanceLifeTime)
            {
                case InstanceLifeTime.InstancePer:
                    break;
                case InstanceLifeTime.InstancePerThread:
                    CallContext.SetData(type.FullName, instance);
                    break;
                case InstanceLifeTime.SingleInstance:
                    info.Instance = instance;
                    break;
                default:
                    break;
            }
            return instance;
        }

        private object CreateInstance(Type type)
        {
            object instance = null;
            var constructors = type.GetConstructors();
            var maxParamsLength = constructors.Max(a => a.GetParameters().Length);
            var pis = constructors.FirstOrDefault(a => a.GetParameters().Length == maxParamsLength).GetParameters();
            if (pis.Length > 0)
            {
                var paramters = new object[pis.Length];
                for (int i = 0; i < pis.Length; i++)
                {
                    paramters[i] = ResolveObject(pis[i].ParameterType);
                }
                instance = Activator.CreateInstance(type, paramters);
            }
            else
            {
                instance = Activator.CreateInstance(type);
            }
            return instance;
        }
    }
}
