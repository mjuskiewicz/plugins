using Autofac;
using PerformanceMonitorService.PluginInfrastructure;

namespace PerformanceMonitorService.Plugins.RamMonitorPlugin
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RamMonitorPlugin>().As<IMonitorPlugin>();
        }
    }
}
