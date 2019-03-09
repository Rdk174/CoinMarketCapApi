namespace BussinesFacade.Interfaces
{
    public interface ISettings
    {
        string GetSettings(string key, string defaultValue);
        string GetSettings(string key);
    }
}