using Orchard;
namespace BigFont.TheThemeMachineDesigner.Services
{
    public interface ITraceTheme : IDependency
    {
        string GetTraceThemeId();

        void SetTraceTheme(string themeName);
    }
}