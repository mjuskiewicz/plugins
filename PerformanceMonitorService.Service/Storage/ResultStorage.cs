using Microsoft.Extensions.Logging;
using PerformanceMonitorService.PluginInfrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PerformanceMonitorService.Service.Storage
{
    public class ResultStorage : IStorageWriter, IStorageReader
    {
        private ILogger<ResultStorage> _logger;

        public ResultStorage(ILogger<ResultStorage> logger)
        {
            _logger = logger;
        }
        private ConcurrentDictionary<Type, object> _storage = new ConcurrentDictionary<Type, object>();

        public void AddOrUpdate(Type key, object value)
        {
            _storage.AddOrUpdate(key, value, (k, v) => value);
            DumpForTestCurrentState();
        }

        public object GetValueOrDefault(Type key)
        {
            return _storage.GetValueOrDefault(key);
        }

        private void DumpForTestCurrentState()
        {
            foreach(KeyValuePair<Type, object> singleEntry in _storage)
            {
                _logger.LogInformation("Key {0} has value : {1}", singleEntry.Key.ToString(), singleEntry.Value.ToString());
            }
        }
    }
}
