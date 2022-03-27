using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoutesList.Build.Enums
{
    public enum TableHeaderControllerActionDescriptor
    {
        [Display(Name = "Method"), Description("Method")]
        Method,

        [Display(Name = "Uri"), Description("Uri")]
        Uri,

        [Display(Name = "Controller name"), Description("Controller Name")]
        Controller,

        [Display(Name = "Action"), Description("Action")]
        Action,

        [Display(Name = "Full Name"), Description("Full Name")]
        Name,
    }
}
