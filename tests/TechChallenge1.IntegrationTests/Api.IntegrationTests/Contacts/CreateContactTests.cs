
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
        ContactDto contact = new ContactDto()
        {   Id = Guid.NewGuid(),
            Name = "Lucas",
            Phone = "11991635199",
            Email = "lucas@test.com",
            
            State = new StateDto() {DDD = 98 , Id = Guid.NewGuid() ,Name = "test"},
        };  
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/contact/register-contact", contact);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    
    }
}
