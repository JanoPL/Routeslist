using Web.Application.Factory;
using Program1 = TestBasicBlazorServer.Program;
using Program2 = TestBasicSiteRazor.Program;
using Program3 = TestBasicSite.Program;

namespace Test.WebApplication.factory
{
    public class CustomWebApplicationTest
    {
        public static IEnumerable<object[]> GetPrograms()
        {
            yield return new object[] { new CustomWebApplication<Program1>() };
            yield return new object[] { new CustomWebApplication<Program2>() };
            yield return new object[] { new CustomWebApplication<Program3>() };
        }

        [Theory]
        [MemberData(nameof(GetPrograms))]
        public async Task WebApplication_Factory_should_create(dynamic application)
        {
            using var client = application?.CreateClient();

            Assert.NotNull(client);
            Assert.IsType<HttpClient>(client);

            using var response = await client?.GetAsync("/");

            response.EnsureSuccessStatusCode();
        }
    }
}