using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class WebHostBuilder : IWebHostBuilder
    {
        private IServer _server;
        private List<Action<IApplicationBuilder>> configures = new List<Action<IApplicationBuilder>>();

        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();
            foreach (var configure in configures)
            {
                configure(builder);
            }

            return new WebHost(_server, builder.Build());
        }

        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            configures.Add(configure);
            return this;
        }

        public IWebHostBuilder UseServer(IServer server)
        {
            _server = server;
            return this;
        }
    }
}
