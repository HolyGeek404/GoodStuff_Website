namespace Website.Services.Other;

public interface IComponentResolver
{
    Type Resolve(string key);
}