using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace Core.Http
{
    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        private HttpListenerContext _httpListenerContext;

        public HttpListenerFeature(HttpListenerContext httpListenerContext) => _httpListenerContext = httpListenerContext;

        public Uri Url => _httpListenerContext.Request.Url;

        NameValueCollection IHttpRequestFeature.Headers => _httpListenerContext.Request.Headers;
        NameValueCollection IHttpResponseFeature.Headers => _httpListenerContext.Response.Headers;

        Stream IHttpRequestFeature.Body => _httpListenerContext.Request.InputStream;
        Stream IHttpResponseFeature.Body => _httpListenerContext.Response.OutputStream;

        public int StatusCode { get => _httpListenerContext.Response.StatusCode; set => _httpListenerContext.Response.StatusCode = value; }
    }
}
