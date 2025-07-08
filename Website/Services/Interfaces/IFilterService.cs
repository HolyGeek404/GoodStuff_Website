namespace Website.Services.Interfaces;

public interface IFilterService
{
    Dictionary<string, List<string>> CreateFilters(List<Dictionary<string, string>> model, string category);
    Dictionary<string, List<string>> Filter(string selectedFilters);
}