﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Swashbuckle.AspNetCore.Filters
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerExamplesFromAssemblyOf<T>(this IServiceCollection services)
        {
            AddSwaggerExamples(services);

            services.Scan(scan => scan
                .FromAssemblyOf<T>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IExamplesProvider<>)))
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .WithSingletonLifetime()
            );
            services.Scan(scan => scan
                .FromAssemblyOf<T>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IMultipleExamplesProvider<>)))
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .WithSingletonLifetime()
            );

            return services;
        }

        public static IServiceCollection AddSwaggerExamplesFromAssemblyOf(this IServiceCollection services, params Type[] types)
        {
            AddSwaggerExamples(services);

            services.Scan(scan => scan
                .FromAssembliesOf(types)
                    .AddClasses(classes => classes.AssignableTo(typeof(IExamplesProvider<>)))
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .WithSingletonLifetime()
            );

            services.Scan(scan => scan
                .FromAssembliesOf(types)
                    .AddClasses(classes => classes.AssignableTo(typeof(IMultipleExamplesProvider<>)))
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .WithSingletonLifetime()
            );

            return services;
        }

        public static IServiceCollection AddSwaggerExamplesFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            AddSwaggerExamples(services);

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IExamplesProvider<>)))
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .WithSingletonLifetime()
            );

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IMultipleExamplesProvider<>)))
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .WithSingletonLifetime()
            );

            return services;
        }

        public static void AddSwaggerExamples(this IServiceCollection services)
        {
            services.AddSingleton<RequestExample>();
            services.AddSingleton<ResponseExample>();
            services.AddSingleton<ExamplesOperationFilter>();
            services.AddSingleton<ServiceProviderExamplesOperationFilter>();
            services.AddSingleton<MvcOutputFormatter>();
        }
    }
}
