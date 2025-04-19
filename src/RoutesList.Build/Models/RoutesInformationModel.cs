#nullable enable
namespace RoutesList.Build.Models
{
    /// <summary>
    /// Represents route information in the application.
    /// </summary>
    public class RoutesInformationModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the route.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        public string? ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        public string? ActionName { get; set; }

        /// <summary>
        /// Gets or sets the display name of the route.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the route template.
        /// </summary>
        public string? Template { get; set; }

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        public string? MethodName { get; set; }

        /// <summary>
        /// Gets or sets the view engine path.
        /// </summary>
        public string? ViewEnginePath { get; set; }

        /// <summary>
        /// Gets or sets the relative path.
        /// </summary>
        public string? RelativePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a compiled page action descriptor.
        /// </summary>
        public bool IsCompiledPageActionDescriptor { get; set; }
    }
}
