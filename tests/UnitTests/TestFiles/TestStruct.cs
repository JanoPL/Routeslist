#if NETCOREAPP3_1 || NET5_0
#endif

namespace UnitTests.TestFiles
{
    internal struct TestStruct
    {
#if NET6_0_OR_GREATER
        public TestStruct()
        {
        }

        public string Test { get; set; } = "Test";
#endif

#if NETCOREAPP3_1 || NET5_0
        string _value;

        public string Test {
            get 
            {
                return _value;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) {
                    _value = "Test";
                }
            }
        }
#endif
    }
}
