using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sample.PDF.Tests
{
    public abstract class WebServerFixture : IDisposable
    {
        private readonly Lazy<TestServer> _serverLazy;

        protected WebServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(AddTestServices)
                .ConfigureAppConfiguration(config => { config.AddJsonFile("appsettings.json"); });

            _serverLazy = new Lazy<TestServer>(() =>
            {
                AddBuilderOptions(builder);
                return new TestServer(builder);
            });
        }

        public TestServer Server => _serverLazy.Value;
        public HttpClient Client => Server.CreateClient();

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }

        protected virtual void AddTestServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvc(o => { o.Filters.Add(new AllowAnonymousFilter()); })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                });
        }

        protected abstract void AddBuilderOptions(IWebHostBuilder builder);
    }
}
