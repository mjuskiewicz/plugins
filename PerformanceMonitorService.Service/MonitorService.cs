using Microsoft.Extensions.Logging;
using PerformanceMonitorService.PluginInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceMonitorService.Service
{
    public class MonitorService
    {
        private readonly ILogger<MonitorService> _logger;
        private readonly IStorageWriter _storage;
        private readonly ICollection<IMonitorPlugin> _plugins;
        private readonly ICollection<IDisposable> _pluginSubscriptions = new List<IDisposable>();

        public MonitorService(
            ILogger<MonitorService> logger,
            IStorageWriter storage,
            IEnumerable<IMonitorPlugin> plugins
        )
        {
            _logger = logger;
            _storage = storage;
            _plugins = plugins.ToList();
            _logger.LogInformation("New performance monitor service was created");
        }

        public void Start()
        {
            _logger.LogInformation("Performance monitor service was started");
            _logger.LogInformation($"Number of registered plugins : {_plugins.Count}");

            ActivateAllPlugins(_plugins);
            SubscribeStorageOnPlugins(_plugins);
        }

        private void SubscribeStorageOnPlugins(IEnumerable<IMonitorPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                var singleSubscription = plugin.ResultStream.Subscribe(UpdateStorageWhenNewValueAvailable);
                _pluginSubscriptions.Add(singleSubscription);
            }
        }

        private void ActivateAllPlugins(IEnumerable<IMonitorPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                try
                {
                    plugin.Active();
                }
                catch
                {
                    //ToDo what should happen when  one of plugins cannot be activated
                    throw;
                }
            }
        }

        private void UpdateStorageWhenNewValueAvailable(object newValue)
        {
            var valueType = newValue.GetType();
            _logger.LogInformation($"Service recived a new value: {valueType.ToString()}");
            _storage.AddOrUpdate(valueType, newValue);
        }

        public void Stop()
        {
            DeactivateAllPlugins(_plugins);
            DisposeAllSubscriptions(_pluginSubscriptions);
                       
            _logger.LogInformation("Performance monitor service was stopped");
        }

        private void DeactivateAllPlugins(IEnumerable<IMonitorPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                plugin.Deactive();
            }
        }

        private void DisposeAllSubscriptions(IEnumerable<IDisposable> pluginSubscriptions)
        {
            foreach (var singleSubscription in pluginSubscriptions)
            {
                singleSubscription.Dispose();
            }
        }
    }
}
