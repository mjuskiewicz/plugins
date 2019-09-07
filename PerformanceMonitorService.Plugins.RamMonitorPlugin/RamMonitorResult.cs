namespace PerformanceMonitorService.Plugins.RamMonitorPlugin
{
    public class RamMonitorResult
    {
        public float Available { get; set; }

        public override string ToString()
        {
            return Available.ToString();
        }
    }
}
