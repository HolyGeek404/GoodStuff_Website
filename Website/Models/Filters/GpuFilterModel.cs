namespace Website.Models.Filters;

public class GpuFilterModel
{
    public int? PriceMin { get; set; }
    public int? PriceMax { get; set; }
    public List<string> Manufacturer = [];
    public List<string> GpuProcessorName = [];
    public List<string> MemorySize = [];
    public List<string> MemoryType = [];
    public List<string> Team = [];
}