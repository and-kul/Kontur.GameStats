﻿using System;
using Fclp;
using Microsoft.Owin.Hosting;

namespace Kontur.GameStats.Server
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            var commandLineParser = new FluentCommandLineParser<Options>();

            commandLineParser
                .Setup(options => options.Prefix)
                .As("prefix")
                .SetDefault("http://+:8080/")
                .WithDescription("HTTP prefix to listen on");

            commandLineParser
                .SetupHelp("h", "help")
                .WithHeader($"{AppDomain.CurrentDomain.FriendlyName} [--prefix <prefix>]")
                .Callback(text => Console.WriteLine(text));

            if (commandLineParser.Parse(args).HelpCalled)
                return;
            
            RunServer(commandLineParser.Object);
        }

        private static void RunServer(Options options)
        {
            using (WebApp.Start<Startup>(options.Prefix))
            {
                Console.WriteLine($"Running on {options.Prefix}");
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }

            
        }

        private class Options
        {
            public string Prefix { get; set; }
        }
    }
}
