using Autofac;
using PerformanceMonitorService.PluginInfrastructure;

namespace PerformanceMonitorService.Plugins.CpuMonitorPlugin
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CpuMonitorPlugin>().As<IMonitorPlugin>();
        }
    }
}
