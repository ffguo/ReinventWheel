using System;
using System.Threading;
using System.Threading.Tasks;

namespace IOC.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.Registered<IUserDAL, UserDAL>(InstanceLifeTime.InstancePerThread);
            container.Registered<ILoginDAL, LoginDAL>(InstanceLifeTime.InstancePerThread);
            container.Registered<IUserServer, UserServer>(InstanceLifeTime.InstancePerThread);
            var userServer1 = container.Resolve<IUserServer>();
            Console.WriteLine(userServer1.Get(2).Name);
            //IUserServer userServer3 = null;
            //IUserServer userServer4 = null;
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //var userServer1 = container.Resolve<IUserServer>();
            //var userServer2 = container.Resolve<IUserServer>();
            //Task.Run(() =>
            //{
            //    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //    userServer3 = container.Resolve<IUserServer>();
            //    userServer4 = container.Resolve<IUserServer>();
            //});
            //Console.WriteLine(ReferenceEquals(userServer1, userServer2));
            //Console.WriteLine(ReferenceEquals(userServer1, userServer3));
            //Console.WriteLine(ReferenceEquals(userServer1, userServer4));
            //Console.WriteLine(ReferenceEquals(userServer3, userServer4));
            Console.ReadKey();
        }
    }
}
