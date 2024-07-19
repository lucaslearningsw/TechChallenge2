using Api.IntegrationTests.Abstractions;
using System.Net;
using FluentAssertions;
using System.Net.Http.Json;
using TechChallenge1.Core.DTO;
using static Api.IntegrationTests.Contacts.CreateContactTests;

namespace Api.IntegrationTests.Contacts;

public class GetContactTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTests(factory)
{


    [Fact(DisplayName = "Get All Contacts")]
    public async Task Should_ReturnOK_WhenListAllContact()
    {
        // Arrange
        await ContactFixture.RegisterContact(HttpClient);
        await ContactFixture.RegisterContact(HttpClient);
        await ContactFixture.RegisterContact(HttpClient);

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync("api/contact/list-contact");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var contacts = await response.Content.ReadFromJsonAsync<List<ContactDto>>();

        contacts!.Count.Should().Be(3);

    }


    [Fact(DisplayName = "Get Contact by id ")]
    public async Task Should_ReturnOK_WhenContactExist()
    {
        // Arrange
        var contact = await ContactFixture.RegisterContact(HttpClient);;

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync($"api/contact/get-by-id/{contact.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var contactResult = await response.Content.ReadFromJsonAsync<ContactDto>();

        contactResult.Should().NotBeNull(); 

    }
}
