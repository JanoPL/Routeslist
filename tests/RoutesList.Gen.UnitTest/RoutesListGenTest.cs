namespace RoutesList.Gen.UnitTest
{
    public class RoutesListGenTest
    {
        private static (ServiceCollection services, IConfigurationRoot configuration) GetServiceConfiguration()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            return (services, configuration);
        }

        [Fact]
        public void UseRoutesListTest1()
        {
            (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

            services.AddRoutesList();

            Assert.True(services.Count >= 1);

            Assert.Collection<ServiceDescriptor>(services,
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Transient, item.Lifetime),
                        () => Assert.Equal(typeof(IRoutes), item.ServiceType)
                ),
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Transient, item.Lifetime),
                        () => Assert.Equal(typeof(ITableBuilder), item.ServiceType)
                ),
                item => Assert.Multiple(
                        () => Assert.Equal(ServiceLifetime.Singleton, item.Lifetime),
                        () => Assert.Equal(typeof(IBuilder), item.ServiceType)
                )
            );
        }
    }
}
