namespace Website.Services.Interfaces;

public interface IFilterService
{
    Dictionary<string, List<string>> CreateFilters(List<Dictionary<string, string>> model, string category);
    List<Dictionary<string, string>> Filter(string selectedFilters);
}