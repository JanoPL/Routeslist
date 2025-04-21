using System;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Extensions
{
    /// <summary>
    /// Provides extension methods for safely adding various route properties to an <see cref="IBuilder"/> instance.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Helper method that adds a value to the builder only if it's not null or empty
        /// </summary>
        /// <param name="builder">The route information builder instance to extend</param>
        /// <param name="value">The value to add</param>
        /// <param name="addMethod">The method to use for adding the value</param>
        /// <returns>The builder instance for method chaining</returns>
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
        
        /// <summary>
        /// Extension method that safely adds an action name to the builder only if it's not null or empty
        /// </summary>
        /// <param name="builder">The route information builder instance to extend</param>
        /// <param name="actionName">The name of the action to add</param>
        /// <returns>The builder instance for method chaining</returns>
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

        /// <summary>
        /// Extension method that safely adds a display name to the builder only if it's not null or empty
        /// </summary>
        /// <param name="builder">The route information builder instance to extend</param>
        /// <param name="displayName">The display name to add</param>
        /// <returns>The builder instance for method chaining</returns>
        public static IBuilder SafeDisplayName(this IBuilder builder, string displayName)
            => AddIfNotEmpty(builder, displayName, (b, v) => b.DisplayName(v));
        
        /// <summary>
        /// Extension method that safely adds a method name to the builder only if it's not null or empty
        /// </summary>
        /// <param name="builder">The route information builder instance to extend</param>
        /// <param name="methodName">The name of the method to add</param>
        /// <returns>The builder instance for method chaining</returns>
        public static IBuilder SafeMethodName(this IBuilder builder, string methodName) 
            => AddIfNotEmpty(builder, methodName, (b, v) => b.MethodName(v));
        
        /// <summary>
        /// Extension method that safely adds a template to the builder only if it's not null or empty
        /// </summary>
        /// <param name="builder">The route information builder instance to extend</param>
        /// <param name="template">The template to add</param>
        /// <returns>The builder instance for method chaining</returns>
        public static IBuilder SafeTemplate(this IBuilder builder, string template) 
            => AddIfNotEmpty(builder, template, (b, v) => b.Template(v));
        
        /// <summary>
        /// Extension method that safely adds a view engine path to the builder only if it's not null or empty
        /// </summary>
        /// <param name="builder">The route information builder instance to extend</param>
        /// <param name="viewEnginePath">The view engine path to add</param>
        /// <returns>The builder instance for method chaining</returns>
        public static IBuilder SafeViewEnginePath(this IBuilder builder, string viewEnginePath) 
            => AddIfNotEmpty(builder, viewEnginePath, (b, v) => b.ViewEnginePath(v));
        
        /// <summary>
        /// Extension method that safely adds a relative path to the builder only if it's not null or empty
        /// </summary>
        /// <param name="builder">The route information builder instance to extend</param>
        /// <param name="relativePath">The relative path to add</param>
        /// <returns>The builder instance for method chaining</returns>
        public static IBuilder SafeRelativePath(this IBuilder builder, string relativePath) 
            => AddIfNotEmpty(builder, relativePath, (b, v) => b.RelativePath(v));
    }
}