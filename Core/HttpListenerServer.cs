using Core.Http;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class HttpListenerServer : IServer
    {
        private readonly string[] _urls;
        private readonly HttpListener _httpListener;

        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            _urls = urls.Length > 0 ? urls : new string[] { "http://localhost:5000/" };
        }

        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));

            _httpListener.Start();
            Console.WriteLine("Server started and is listening on: {0}", string.Join(';', _urls));
            while (true)
            {
                var httpListenerContext = await _httpListener.GetContextAsync();
                var feature = new HttpListenerFeature(httpListenerContext);
                var features = new FeatureCollection()
                            .Set<IHttpRequestFeature>(feature)
                            .Set<IHttpResponseFeature>(feature);
                var httpContext = new HttpContext(features);
                await handler(httpContext);
                httpListenerContext.Response.Close();
            }
        }
    }
}
