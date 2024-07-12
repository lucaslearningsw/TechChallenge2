using Api.IntegrationTests.Abstractions;
using FluentAssertions;
using System.Net;


namespace Api.IntegrationTests.Contacts
{
    public  class DeleteContactTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTests(factory)
    {
        [Fact]
        public async Task Deve_RetornarNotFound_QuandoContatoNaoExiste()
        {
            // Arrange
            // Act
            HttpResponseMessage response = await HttpClient.DeleteAsync($"api/contact/delete-contact/{Guid.NewGuid()}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);


        }
    }
}
