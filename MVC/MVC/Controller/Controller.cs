using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public abstract class Controller : ControllerBase
    {

        protected virtual ContentResult Content(string content)
        {
            return Content(content, null);
        }

        protected virtual ContentResult Content(string content, string contentType)
        {
            return Content(content, contentType, null);
        }

        protected virtual ContentResult Content(string content, string contentType, Encoding contentEncoding)
        {
            return new ContentResult()
            {
                Content = content,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

        protected virtual JsonResult Json(object data, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
        {
            var jsonResult = new JsonResult
            {
                Data = data,
                JsonRequestBehavior = jsonRequestBehavior
            };
            return jsonResult;
        }

        protected virtual ViewResult View(object data)
        {
            var viewResult = new ViewResult
            {
                Data = data
            };
            return viewResult;
        }

    }
}
