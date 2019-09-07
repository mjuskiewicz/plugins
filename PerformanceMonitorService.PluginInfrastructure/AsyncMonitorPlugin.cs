using Microsoft.Extensions.Logging;
using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace PerformanceMonitorService.PluginInfrastructure
{
    public abstract class AsyncMonitorPlugin<TPlugin, TMetric> : IMonitorPlugin
        where TMetric : class
    {
        private CancellationTokenSource _taskTokenSource;

        protected Subject<TMetric> _resultStream = new Subject<TMetric>();
        protected ILogger<TPlugin> _logger;

        public IObservable<object> ResultStream => _resultStream;

        public AsyncMonitorPlugin(ILogger<TPlugin> logger)
        {
            _logger = logger;
        }

        public void Active()
        {
            if (_taskTokenSource != null) throw new Exception("Plugin can be activated only once");

            _taskTokenSource = new CancellationTokenSource();

            var newTask = new Task(() => PluginWork(), _taskTokenSource.Token, TaskCreationOptions.LongRunning);
            newTask.Start();
        }

        protected abstract void PluginWork();

        public void Deactive()
        {
            _taskTokenSource.Cancel();
        }
    }
}
