namespace Website.Services.Filters;

public interface IFilterService
{
    Dictionary<string, List<string>> GetFilters(List<Dictionary<string, string>> model, string category);
    List<Dictionary<string, string>> Filter(List<Dictionary<string, string>> model, Dictionary<string, List<string>> selectedFilters, string category);
}