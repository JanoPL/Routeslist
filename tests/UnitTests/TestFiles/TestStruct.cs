using System;
#if NETCOREAPP3_1 || NET5_0
#endif

namespace UnitTests.TestFiles
{
    struct TestStruct : IEquatable<TestStruct>
    {
        
        private string _value;

        public string Test {
            get => _value;
            set
            {
                if (string.IsNullOrEmpty(value)) {
                    _value = "Test";
                }
            }
        }

        public bool Equals(TestStruct other)
        {
            return _value == other._value;
        }

        public override bool Equals(object? obj)
        {
            return obj is TestStruct other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
