using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoutesList.Build.Enums
{
    /// <summary>
    /// Describes the column headers for the page action table.
    /// </summary>
    public enum TableHeaderPageActionDescriptor
    {
        /// <summary>
        /// Full name of the page action.
        /// </summary>
        [Display(Name = "Full Name")]
        Name,

        /// <summary>
        /// Relative path of the page action.
        /// </summary>
        [Display(Name = "Relative Path")]
        RelativePath,

        /// <summary>
        /// Relative engine path of the page action.
        /// </summary>
        [Display(Name = "Relative Engine Path")]
        ViewEnginePath
    }
}
