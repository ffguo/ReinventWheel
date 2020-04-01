using ORM.Expressions;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace ORM.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 配置数据库连接
            var configuration = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .Build();
            SqlConnectionPool.Init(configuration["ConnectionsStr:Write"], configuration.GetSection("ConnectionsStr").GetSection("Read").GetChildren().Select(a => a.Value).ToList());

            {
                var blogDal = new MyORM<Blogs>();
                for (int i = 0; i < 10; i++)
                {
                    var blog = blogDal.Find(1);
                }
            }

            {
                var blogDal = new MyORM<Blogs>();
                blogDal.Insert(new Blogs
                {
                    Name = "测试ORM插入",
                    CreateTime = DateTime.Now,
                    ModifiedTime = DateTime.Now,
                    Url = "www.google.com",
                    IsDeleted = false
                });

                //for (int i = 1; i < 100; i++)
                //{

                //    var blog = blogDal.Find(1004);
                //    if (blog == null)
                //        Console.WriteLine($"第{i}未获取到");
                //    else
                //        break;
                //    Thread.Sleep(500);
                //}
                //Console.WriteLine("获取到了");
            }

            {
                //var blogDal = new MyORM<Blogs>();
                //var blog = blogDal.Find(2);
                //blog.Name += "123";
                //blogDal.Update(blog);
            }

            {
                //var blogDal = new MyORM<Blogs>();
                //blogDal.Delete(2);
            }

            {
                //var blogDal = new MyORM<Blogs>();
                //var blog = blogDal.Find(3);
                //blog.Name += "123";
                //string name = "123";
                //blogDal.UpdateCondition(new Blogs { IsDeleted = true }, a => a.Name == name && a.IsDeleted == true);
            }

            {
                //var name = Console.ReadLine();
                //var isDelete = Console.ReadLine() == "Y";

                //var query = new MyORM<Blogs>().Table;
                //if (!string.IsNullOrEmpty(name))
                //    query = query.Where(a => a.Name == name);
                //query = query.Where(a => a.IsDeleted == isDelete);
                //var list = query.ToList();
                //foreach (var item in list)
                //{
                //    Console.WriteLine(item.Name + "-" + item.CreateTime);
                //}
            }

            Console.ReadKey();
        }
    }
}
