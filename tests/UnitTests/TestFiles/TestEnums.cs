#if NETCOREAPP3_1 || NET5_0
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#endif

namespace UnitTests.TestFiles
{
    internal enum TestEnums
    {
        [Display(Name = "Test Name 1"), Description("Test Description 1")]
        Test1,

        [Display(Name = "Test Name 2"), Description("Test Description 2")]
        Test2,

        [Display(Name = "Test Name 3")]
        Test3,
    }
}