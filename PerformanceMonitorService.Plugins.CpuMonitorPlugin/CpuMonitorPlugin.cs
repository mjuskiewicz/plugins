using Microsoft.Extensions.Logging;
using PerformanceMonitorService.PluginInfrastructure;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PerformanceMonitorService.Plugins.CpuMonitorPlugin
{
    public class CpuMonitorPlugin : AsyncMonitorPlugin<CpuMonitorPlugin, CpuMonitorResult>
    {
        private const string CounterCategoryName = "Processor";
        private const string CounterName = "% Processor Time";
        private const string CounterInstanceName = "_Total";

        public CpuMonitorPlugin(ILogger<CpuMonitorPlugin> logger)
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
                await Task.Delay(1000);

            } while (true);

        }

        private CpuMonitorResult Measure()
        {
            _logger.LogInformation("Start of measurement.");

            var cpuMonitor = new PerformanceCounter
            {
                CategoryName = CounterCategoryName,
                CounterName = CounterName,
                InstanceName = CounterInstanceName
            };
            var usageOfCpu = cpuMonitor.NextValue();

            _logger.LogInformation($"Total cpu usage : {usageOfCpu}%");
            _logger.LogInformation("End of measurement.");

            return new CpuMonitorResult { UsageOfCpu = usageOfCpu };
        }       
    }
}
