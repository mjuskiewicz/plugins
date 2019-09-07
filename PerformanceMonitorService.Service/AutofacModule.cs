using Autofac;
using PerformanceMonitorService.PluginInfrastructure;
using PerformanceMonitorService.Service.Storage;

namespace PerformanceMonitorService.Service
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MonitorService>();
            builder.RegisterType<ResultStorage>()
                   .As<IStorageWriter>()
                   .As<IStorageReader>()                   
                   .SingleInstance();
        }
    }
}
