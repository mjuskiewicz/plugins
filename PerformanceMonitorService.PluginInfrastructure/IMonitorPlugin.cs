using System;

namespace PerformanceMonitorService.PluginInfrastructure
{
    public interface IMonitorPlugin
    {
        IObservable<object> ResultStream { get; }
        void Active();
        void Deactive();
    }
}
