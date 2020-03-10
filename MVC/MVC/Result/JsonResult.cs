﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace MVC
{
    public class JsonResult : ActionResult
    {
        public JsonResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        public JsonRequestBehavior JsonRequestBehavior { get; set; }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext controllerContext)
        {
            HttpResponse response = controllerContext.HttpContext.Response;
            if (!string.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var json = jss.Serialize(Data);
            response.Write(json);
        }
    }

    public enum JsonRequestBehavior
    {
        AllowGet,
        DenyGet,
    }
}
