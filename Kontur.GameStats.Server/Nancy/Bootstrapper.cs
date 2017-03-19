using System;
using Kontur.GameStats.Server.JSON;
using Kontur.GameStats.Server.StatisticsManagement;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace Kontur.GameStats.Server.Nancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        //protected override DiagnosticsConfiguration DiagnosticsConfiguration
        //    => new DiagnosticsConfiguration {Password = @"Innopolis"};

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<JsonSerializer, CamelCaseJsonSerializer>();
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            StatisticsProcessor.Start();
            
            pipelines.BeforeRequest += ctx =>
            {
                ctx.Request.Headers.Accept = new[] {Tuple.Create("application/json", 1m)};
                ctx.Request.Headers.ContentType = "application/json";

                return null;
            };
            

            //StaticConfiguration.EnableRequestTracing = true;
            //StaticConfiguration.DisableErrorTraces = false;
        }
    }
}