using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace Core.Http
{
    public class HttpResponse
    {
        private readonly IHttpResponseFeature _feature;

        public HttpResponse(IFeatureCollection features) => _feature = features.Get<IHttpResponseFeature>();

        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;
        public int StatusCode { get => _feature.StatusCode; set => _feature.StatusCode = value; }
    }
}
