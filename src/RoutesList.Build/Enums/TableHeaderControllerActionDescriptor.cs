using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoutesList.Build.Enums
{
    /// <summary>
    /// Defines the column headers for controller action descriptor table representation.
    /// </summary>
    public enum TableHeaderControllerActionDescriptor
    {
        /// <summary>
        /// The HTTP method of the endpoint.
        /// </summary>
        [Display(Name = "HTTP Method"), Description("HTTP Method")]
        Method,

        /// <summary>
        /// The URI pattern of the endpoint.
        /// </summary>
        [Display(Name = "URI Pattern"), Description("URI Pattern")]
        Uri,

        /// <summary>
        /// The name of the controller.
        /// </summary>
        [Display(Name = "Controller"), Description("Controller")]
        Controller,

        /// <summary>
        /// The name of the action method.
        /// </summary>
        [Display(Name = "Action Method"), Description("Action Method")]
        Action,

        /// <summary>
        /// The full qualified name of the endpoint.
        /// </summary>
        [Display(Name = "Full Name"), Description("Full Qualified Name")]
        Name,
    }
}
