using System;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Extensions
{
    public static class BuilderExtensions
    {
        private static IBuilder AddIfNotEmpty(
            this IBuilder builder,
            string value,
            Func<IBuilder, string,IBuilder> addMethod
        ) {
            if (!String.IsNullOrEmpty(value)) {
                addMethod(builder, value);
            }

            return builder;
        }
        
        public static IBuilder SafeActionName(this IBuilder builder, string actionName)
            => AddIfNotEmpty(builder, actionName, (b, v) => b.ActionName(v));

        /// <summary>
        /// Extension method that safely adds a controller name to the builder only if it's not null or empty
        /// </summary>
        /// <param name="builder">The route information builder instance to extend</param>
        /// <param name="controllerName">The name of the controller to add</param>
        /// <returns>The builder instance for method chaining</returns>
        public static IBuilder SafeControllerName(this IBuilder builder, string controllerName)
            => AddIfNotEmpty(builder, controllerName, (b, v) => b.ControllerName(v));

        public static IBuilder SafeDisplayName(this IBuilder builder, string displayName)
            => AddIfNotEmpty(builder, displayName, (b, v) => b.DisplayName(v));
        
        public static IBuilder SafeMethodName(this IBuilder builder, string methodName) 
            => AddIfNotEmpty(builder, methodName, (b, v) => b.MethodName(v));
        
        public static IBuilder SafeTemplate(this IBuilder builder, string template) 
            => AddIfNotEmpty(builder, template, (b, v) => b.Template(v));
        
        public static IBuilder SafeViewEnginePath(this IBuilder builder, string viewEnginePath) 
            => AddIfNotEmpty(builder, viewEnginePath, (b, v) => b.ViewEnginePath(v));
        
        public static IBuilder SafeRelativePath(this IBuilder builder, string relativePath) 
            => AddIfNotEmpty(builder, relativePath, (b, v) => b.RelativePath(v));
    }
}