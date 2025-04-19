using System;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    public abstract class AbstractBuilderMethod
    {
        protected static void AddActionName(string actionName, Builder builder)
        {
            if (!String.IsNullOrEmpty(actionName)) {
                builder.ActionName(actionName);
            }
        }

        protected static void AddControllerName(string controllerName, Builder builder)
        {
            if (!String.IsNullOrEmpty(controllerName)) {
                builder.ControllerName(controllerName);
            }
        }

        protected static void AddDisplayName(string displayName, Builder builder)
        {
            if (!String.IsNullOrEmpty(displayName)) {
                builder.DisplayName(displayName);
            }
        }

        protected static void AddMethodName(string methodName, Builder builder)
        {
            if (!String.IsNullOrEmpty(methodName)) {
                builder.MethodName(methodName);
            }
        }

        protected static void AddRelativePath(string relativePath, Builder builder)
        {
            if (!String.IsNullOrEmpty(relativePath)) {
                builder.RelativePath(relativePath);
            }
        }

        protected static void AddTemplate(string template, Builder builder)
        {
            if (!String.IsNullOrEmpty(template)) {
                builder.Template(template);
            }
        }

        protected static void AddViewEnginePath(string viewEnginePath, Builder builder)
        {
            if (!String.IsNullOrEmpty(viewEnginePath)) {
                builder.ViewEnginePath(viewEnginePath);
            }
        }
    }
}
