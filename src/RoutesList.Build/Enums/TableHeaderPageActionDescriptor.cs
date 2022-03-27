using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoutesList.Build.Enums
{
    public enum TableHeaderPageActionDescriptor
    {
        [Display(Name = "Full Name"), Description("Full Name")]
        Name,

        [Display(Name = "Relative Engine Path"), Description("Relative Engine Path")]
        ViewEnginePath,
        
        [Display(Name = "Relative Path"), Description("Relative Path")]
        RelativePath,
    }
}
