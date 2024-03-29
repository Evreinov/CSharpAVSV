﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrafficLights.lib.Interfaces;
using TrafficLights.lib.ViewModels;
using TrafficLights.ViewModels;

namespace TrafficLights
{
    public partial class App
    {
        private static IHost _Hosting;

        public static IHost Hosting => _Hosting
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
            .ConfigureServices(ConfigureServices)
            .Build();

        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<TrafficLightsViewModel>();
            services.AddTransient<PedestrianTrafficLightsViewModel>();
        }
    }
}
