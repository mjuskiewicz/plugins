using System;

namespace PerformanceMonitorService.PluginInfrastructure
{
    public interface IStorageWriter
    {
        void AddOrUpdate(Type key, object value);
    }
}
