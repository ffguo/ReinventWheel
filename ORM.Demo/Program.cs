using System;

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
                var blogDal = new MyORM<Blogs>();
                //var blog = blogDal.Find(3);
                //blog.Name += "123";
                blogDal.UpdateCondition(new Blogs { IsDeleted = true }, a => a.Name == "123");
            }

            Console.ReadKey();
        }
    }
}
