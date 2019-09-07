using Autofac;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using PerformanceMonitorService.Service;
using System;
using Topshelf;
using Topshelf.Autofac;

namespace PerformanceMonitorService.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = BuildContainer();

            var rc = HostFactory.Run(configure =>
            {
                configure.UseNLog();
                configure.UseAutofacContainer(container);

                configure.Service<MonitorService>(service =>
                {
                    service.ConstructUsingAutofacContainer();
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });

                configure.RunAsLocalSystem();

                configure.SetServiceName("PerformanceMonitorService");
                configure.SetDisplayName("PerformanceMonitorService");
                configure.SetDescription("Service measure a basic computer parameters.");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }

        static IContainer BuildContainer()
        {
            var builderForContainer = new ContainerBuilder();
                builderForContainer.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));
                builderForContainer.RegisterType<NLogLoggerFactory>().AsImplementedInterfaces().InstancePerLifetimeScope();
                builderForContainer.RegisterModule(new PerformanceMonitorService.Service.AutofacModule());
                builderForContainer.RegisterModule(new PerformanceMonitorService.Plugins.RamMonitorPlugin.AutofacModule());
                builderForContainer.RegisterModule(new PerformanceMonitorService.Plugins.CpuMonitorPlugin.AutofacModule());

            return builderForContainer.Build();
        }
    }  
}
