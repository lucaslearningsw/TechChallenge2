namespace Api.IntegrationTests.Abstractions;


public abstract class BaseIntegrationTests : IClassFixture<IntegrationTestWebAppFactory>
{
    protected HttpClient HttpClient { get; init; }

    protected BaseIntegrationTests(IntegrationTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }
}

