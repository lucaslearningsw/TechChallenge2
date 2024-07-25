
using Api.IntegrationTests.Abstractions;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;
using TechChallenge1.Core.DTO;


namespace Api.IntegrationTests.Contacts;

public  class CreateContactTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTests(factory)
{
    [Fact]
    public async Task Should_ReturnOK_WhenContactRegistered()
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

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contact/register-contact", contact);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }



    [Fact]
    public async Task Should_ReturnBadRequest_WhenEmailIsNotValid()
    {
        // Arrange
        // Act
        ContactDto contact = new ContactDto()
        {
            Id = Guid.NewGuid(),
            Name = "Lucas",
            Phone = "11991635199",
            Email = "lucas@",

            State = new StateDto() { DDD = 98, Id = Guid.NewGuid(), Name = "test" },
        };

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contact/register-contact", contact);
        var returnValue = response.Content.ReadAsStringAsync().Result.ToString();

        // Assert
         response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        returnValue.Contains("Informe um endereco de e-mail valido. Ex.: nome@dominio.com.br").Should().BeTrue();
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenPHoneNumberIsInvalid()
    {
        // Arrange
        // Act
        ContactDto contact = new ContactDto()
        {
            Id = Guid.NewGuid(),
            Name = "Lucas",
            Phone = "1199163",
            Email = "lucas@test.com",

            State = new StateDto() { DDD = 98, Id = Guid.NewGuid(), Name = "test" },
        };

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contact/register-contact", contact);
        var returnValue = response.Content.ReadAsStringAsync().Result.ToString();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        returnValue.Contains("O telefone deve conter entre 10 e 11 digitos.").Should().BeTrue();
    }


    [Fact]
    public async Task Should_ReturnBadRequest_WhenNameIsNotFilled()
    {
        // Arrange
        // Act
        ContactDto contact = new ContactDto()
        {
            Id = Guid.NewGuid(),
            Name = "",
            Phone = "11991635199",
            Email = "lucas@test.com",

            State = new StateDto() { DDD = 98, Id = Guid.NewGuid(), Name = "test" },
        };

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contact/register-contact", contact);
        var returnValue = response.Content.ReadAsStringAsync().Result.ToString();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        returnValue.Contains("Campo de preenchimento obrigatorio.").Should().BeTrue();
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenStateIdDoesNotExist()
    {
        // Arrange
        // Act
        ContactDto contact = new ContactDto()
        {
            Id = Guid.NewGuid(),
            Name = "lucastest",
            Phone = "11991635199",
            Email = "lucas@test.com",

            State = new StateDto() { DDD = 9899, Id = Guid.NewGuid(), Name = "test" },
        };

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contact/register-contact", contact);
        var returnValue = response.Content.ReadAsStringAsync().Result.ToString();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        returnValue.Contains("DDD não existe").Should().BeTrue();
    }



    public static class ContactFixture
    {
        public static async Task<ContactDto> RegisterContact(HttpClient HttpClient)
        {
            // Arrange
            // Act
            ContactDto contact = new ContactDto()
            {
                Id = Guid.NewGuid(),
                Name = "Lucas",
                Phone = "11991635199",
                Email = "lucas@test.com",

                State = new StateDto() { DDD = 98, Id = Guid.NewGuid(), Name = "test" },
            };

            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/contact/register-contact", contact);

            ContactDto contactResult = await response.Content.ReadFromJsonAsync<ContactDto>();

            return contactResult;
        }
    }


}
