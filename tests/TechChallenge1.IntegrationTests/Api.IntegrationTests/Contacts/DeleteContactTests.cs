using Api.IntegrationTests.Abstractions;
using FluentAssertions;
using System.Net;
using static Api.IntegrationTests.Contacts.CreateContactTests;


namespace Api.IntegrationTests.Contacts
{
    public  class DeleteContactTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTests(factory)
    {
        [Fact]
        public async Task Should_Return_NotFound_WhenContactDoesNotExist()
        {
            // Arrange
            // Act
            HttpResponseMessage response = await HttpClient.DeleteAsync($"api/contact/delete-contact/{Guid.NewGuid()}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_ReturnOk_WhenContactExist()
        {
            // Arrange
            var contact = await ContactFixture.RegisterContact(HttpClient);

            // Act
            HttpResponseMessage response = await HttpClient.DeleteAsync($"api/contact/delete-contact/{contact.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
