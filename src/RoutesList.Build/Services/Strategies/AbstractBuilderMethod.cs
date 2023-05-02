using System;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    public abstract class AbstractBuilderMethod
    {
        public static void AddActionName(string actionName, Builder builder)
        {
            if (!String.IsNullOrEmpty(actionName)) {
                builder.ActionName(actionName);
            }
        }

        public static void AddControllerName(string controllerName, Builder builder)
        {
            if (!String.IsNullOrEmpty(controllerName)) {
                builder.ControllerName(controllerName);
            }
        }

        public static void AddDisplayName(string displayName, Builder builder)
        {
            if (!String.IsNullOrEmpty(displayName)) {
                builder.DisplayName(displayName);
            }
        }

        public static void AddMethodName(string methodName, Builder builder)
        {
            if (!String.IsNullOrEmpty(methodName)) {
                builder.MethodName(methodName);
            }
        }
        public static void AddRelativePath(string relativePath, Builder builder)
        {
            if (!String.IsNullOrEmpty(relativePath)) {
                builder.RelativePath(relativePath);
            }
        }

        public static void AddTemplate(string template, Builder builder)
        {
            if (!String.IsNullOrEmpty(template)) {
                builder.Template(template);
            }
        }

        public static void AddViewEnginePath(string viewEnginePath, Builder builder)
        {
            if (!String.IsNullOrEmpty(viewEnginePath)) {
                builder.ViewEnginePath(viewEnginePath);
            }
        }
    }
}
