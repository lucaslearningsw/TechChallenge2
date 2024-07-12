using Api.IntegrationTests.Abstractions;
using System.Net;
using FluentAssertions;
using System.Net.Http.Json;
using TechChallenge1.Core.DTO;

namespace Api.IntegrationTests.Contacts;

public class GetContactTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTests(factory)
{

    [Fact(DisplayName = "Get Contact that does not exist")]
    public async Task Should_ReturnNotFound_WhenContactDoesNotExist()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync($"api/contact/list-contact{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Fact(DisplayName = "Get All Contacts")]
    public async Task Should_ReturnOK_WhenListAllContact()
    {
        // Arrange
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync("api/contact/list-contact");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var contacts = await response.Content.ReadFromJsonAsync<List<ContactDto>>();

        contacts!.Count.Should().Be(3);

    }
}
