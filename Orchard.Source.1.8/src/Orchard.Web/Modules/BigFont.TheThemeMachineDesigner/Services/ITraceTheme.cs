using Orchard;
namespace BigFont.TheThemeMachineDesigner.Services
{
    public interface ITraceTheme : IDependency
    {
        string GetTraceTheme();

        void SetTraceTheme(string themeName);
    }
}