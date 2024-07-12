
using Api.IntegrationTests.Abstractions;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;
using TechChallenge1.Core.DTO;

namespace Api.IntegrationTests.Contacts;

public  class CreateContactTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTests(factory)
{
    [Fact]
    public async Task Deve_RetornarNotFound_QuandoContatoNaoExiste()
    {
        // Arrange
        // Act
        ContactDto contact = new ContactDto() { Email = "lucas@test.com", Id = Guid.NewGuid(), Name = "Lucas", Phone = "999999999", StateId = Guid.NewGuid()};  
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/contact/register-contact", contact);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    
    }
}
