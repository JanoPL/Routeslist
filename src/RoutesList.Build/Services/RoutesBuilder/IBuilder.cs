using RoutesList.Build.Models;

namespace RoutesList.Build.Services.RoutesBuilder
{
    /// <summary>
    /// Builder interface for constructing route information models.
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Initializes a new route builder with the specified ID.
        /// </summary>
        /// <param name="id">The optional route identifier.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IBuilder Create(int? id);

        /// <summary>
        /// Sets the controller name for the route.
        /// </summary>
        /// <param name="name">The name of the controller.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IBuilder ControllerName(string name);

        /// <summary>
        /// Sets the action name for the route.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IBuilder ActionName(string name);

        /// <summary>
        /// Sets the display name for the route.
        /// </summary>
        /// <param name="name">The display name of the route.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IBuilder DisplayName(string name);

        /// <summary>
        /// Sets the method name for the route.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IBuilder MethodName(string name);

        /// <summary>
        /// Sets the template for the route.
        /// </summary>
        /// <param name="templateName">The name of the template.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IBuilder Template(string templateName);

        /// <summary>
        /// Sets the view engine path for the route.
        /// </summary>
        /// <param name="enginePath">The path to the view engine.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IBuilder ViewEnginePath(string enginePath);

        /// <summary>
        /// Sets the relative path for the route.
        /// </summary>
        /// <param name="path">The relative path.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IBuilder RelativePath(string path);

        /// <summary>
        /// Sets whether the route is a compiled page action descriptor.
        /// </summary>
        /// <param name="isCompiled">True if the page is compiled; otherwise, false.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IBuilder IsCompiledPageActionDescriptior(bool isCompiled);

        /// <summary>
        /// Builds and returns the final routes information model.
        /// </summary>
        /// <returns>The constructed routes information model.</returns>
        RoutesInformationModel Build();
    }
}