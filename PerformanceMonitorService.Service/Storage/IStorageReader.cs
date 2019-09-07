using System;

namespace PerformanceMonitorService.PluginInfrastructure
{
    public interface IStorageReader
    {
        object GetValueOrDefault(Type key);
    }
}
