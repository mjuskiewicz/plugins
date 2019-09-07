using Microsoft.Extensions.Logging;
using PerformanceMonitorService.PluginInfrastructure;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PerformanceMonitorService.Plugins.RamMonitorPlugin
{
    public class RamMonitorPlugin : AsyncMonitorPlugin<RamMonitorPlugin, RamMonitorResult>
    {
        private const string CounterCategoryName = "Memory";
        private const string CounterName = "Available MBytes";

        public RamMonitorPlugin(ILogger<RamMonitorPlugin> logger)
            : base(logger)
        {
            _logger.LogInformation("Ram Monitor Plugin was setup.");
        }

        protected override async void PluginWork()
        {
            do
            {
                var newResult = Measure();
                _resultStream.OnNext(newResult);
                await Task.Delay(1500);

            } while (true);

        }

        private RamMonitorResult Measure()
        {
            var ramMonitor = new PerformanceCounter(CounterCategoryName, CounterName);
            var availableRam = ramMonitor.NextValue();

            _logger.LogInformation($"Available Ram : {availableRam}.");

            return new RamMonitorResult { Available = availableRam };
        }
    }
}
