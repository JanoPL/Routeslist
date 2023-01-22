namespace UnitTests.Extensions.Tests
{
    public class EnumExtensionTests
    {
        [Fact]
        public void ToDictionaryTest()
        {
            var names = EnumExtension.ToDictionary(TestEnums.Test1);

            Assert.Collection(names,
                item => Assert.Equal(0, item.Key),
                item => Assert.Equal(1, item.Key),
                item => Assert.Equal(2, item.Key)
            );

            Assert.Collection(names,
                item => Assert.Equal("Test Name 1", item.Value),
                item => Assert.Equal("Test Name 2", item.Value),
                item => Assert.Equal("Test Name 3", item.Value)
            );
        }

        [Fact]
        public void GetListOfDescriptionTest()
        {
            var listDescriptions = EnumExtension.GetListOfDescription<TestEnums>();

            Assert.Collection(listDescriptions,
                item => Assert.Equal("Test Description 1", item),
                item => Assert.Equal("Test Description 2", item),
                Assert.Null
            );
        }

        [Fact]
        public void GetListOfDescriptionNullTest()
        {
            var listDescriptions = EnumExtension.GetListOfDescription<TestStruct>();

            Assert.Null(listDescriptions);
        }

        [Fact]
        public void GetDescriptionTest()
        {
            var Description = EnumExtension.GetDescription(TestEnums.Test1);

            Assert.Equal("Test Description 1", Description);
        }

        [Fact]
        public void GetDescriptionNullTest()
        {
            var Description = EnumExtension.GetDescription(TestEnums.Test3);

            Assert.Null(Description);
        }
    }
}