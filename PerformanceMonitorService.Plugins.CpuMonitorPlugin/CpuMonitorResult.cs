namespace PerformanceMonitorService.Plugins.CpuMonitorPlugin
{
    public class CpuMonitorResult
    {
        public float UsageOfCpu { get; set; }

        public override string ToString()
        {
            return UsageOfCpu.ToString();
        }
    }
}
