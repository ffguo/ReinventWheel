using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC;
using MVC.Demo.Models;

namespace MVC.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int id, string name)
        {
            return Content($"参数id是{id}, name是{name}");
        }

        public ActionResult JsonTest(int id, string name)
        {
            var student = new Student
            {
                Id = id,
                Name = name
            };
            return Json(student);
        }

        public ActionResult ViewTest(int id, string name)
        {
            var student = new Student
            {
                Id = id,
                Name = name
            };
            return View(student);
        }
    }
}