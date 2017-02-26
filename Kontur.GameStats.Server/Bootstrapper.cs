﻿using System;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Kontur.GameStats.Server
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        //protected override DiagnosticsConfiguration DiagnosticsConfiguration
        //    => new DiagnosticsConfiguration {Password = @"Innopolis"};


        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
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