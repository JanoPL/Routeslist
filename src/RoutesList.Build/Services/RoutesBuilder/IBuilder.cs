using RoutesList.Build.Models;

namespace RoutesList.Build.Services.RoutesBuilder
{
    public interface IBuilder
    {
        IBuilder Create(int? id);
        IBuilder ControllerName(string name);
        IBuilder ActionName(string name);
        IBuilder DisplayName(string name);
        IBuilder MethodName(string name);
        IBuilder Template(string templateName);
        IBuilder ViewEnginePath(string enginePath);
        IBuilder RelativePath(string path);
        IBuilder IsCompiledPageActionDescriptior(bool isCompiled);
        RoutesInformationModel Build();
    }
}