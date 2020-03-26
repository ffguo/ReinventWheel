using ORM.Expressions;
using System;
using System.Linq;

namespace ORM.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var blogDal = new MyORM<Blogs>();
                var blog = blogDal.Find(1);
            }

            {
                //var blogDal = new MyORM<Blogs>();
                //blogDal.Insert(new Blogs
                //{
                //    Name = "测试ORM",
                //    CreateTime = DateTime.Now,
                //    ModifiedTime = DateTime.Now,
                //    Url = "www.baidu.com",
                //    IsDeleted = false
                //});
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
                var name = Console.ReadLine();
                var isDelete = Console.ReadLine() == "Y";

                var query = new MyORM<Blogs>().Table;
                if (!string.IsNullOrEmpty(name))
                    query = query.Where(a => a.Name == name);
                query = query.Where(a => a.IsDeleted == isDelete);
                var list = query.ToList();
                foreach (var item in list)
                {
                    Console.WriteLine(item.Name + "-" + item.CreateTime);
                }
            }

            Console.ReadKey();
        }
    }
}
