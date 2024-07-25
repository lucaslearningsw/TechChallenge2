
using Api.IntegrationTests.Abstractions;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TechChallenge1.Core.DTO;
using static Api.IntegrationTests.Contacts.CreateContactTests;

namespace Api.IntegrationTests.Contacts
{
    public class UpdateContactsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTests(factory)
    {
        [Fact]
        public async Task Should_ReturnNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            // Act
            ContactDto contact = new()
            {
                Id = Guid.NewGuid(),
                Name = "Lucas",
                Phone = "11991635199",
                Email = "lucas@test.com",

                State = new StateDto() { DDD = 98, Id = Guid.NewGuid(), Name = "test" },
            };

            HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/contact/update-contact", contact);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenEmailIsNotValid()
        {
            // Arrange
            ContactDto contactRegister = await ContactFixture.RegisterContact(HttpClient);
            // Act

            contactRegister.Email = "lucas";

            HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/contact/update-contact", contactRegister);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var returnValue = response.Content.ReadAsStringAsync().Result.ToString();

            returnValue.Contains("Informe um endereco de e-mail valido. Ex.: nome@dominio.com.br").Should().BeTrue();

        }

        [Fact]
        public async Task Should_ReturnOk_WhenUpdateContactIsValid()
        {
            // Arrange
            ContactDto contactRegister = await ContactFixture.RegisterContact(HttpClient);
            // Act

            contactRegister.Email = "lucas@gmail.com";
            contactRegister.Phone = "11991999999";

            HttpResponseMessage response = await HttpClient.PutAsJsonAsync("api/contact/update-contact", contactRegister);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }
    }
}
