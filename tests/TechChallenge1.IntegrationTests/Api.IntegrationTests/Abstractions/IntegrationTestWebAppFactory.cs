using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TechChallenge1.Data.Context;
using Testcontainers.MsSql;

namespace Api.IntegrationTests.Abstractions;
public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(async services =>
        {
            services.RemoveAll(typeof(DbContextOptions<techchallengeDbContext>));

            services.AddDbContext<techchallengeDbContext>(options =>
                options
                    .UseSqlServer(_msSqlContainer.GetConnectionString()));

            await _msSqlContainer.ExecScriptAsync(CreateTables());
        });
    }

    private string CreateTables()
    {
        string sql =
            """
           
            CREATE TABLE [TechChallenge1].[Contact](
            	[Id] [uniqueidentifier] NOT NULL,
            	[Name] [varchar](100) NOT NULL,
            	[Phone] [varchar](100) NOT NULL,
            	[Email] [varchar](100) NOT NULL,
            	[StateId] [uniqueidentifier] NOT NULL
               CONSTRAINT [PK_Contatos] PRIMARY KEY ([Id]),
               CONSTRAINT [FK_Contatos_State_Id] FOREIGN KEY ([Id]) REFERENCES [State] ([Id]) ON DELETE CASCADE
            );
           
            CREATE TABLE [TechChallenge1].[State](
            	[Id] [uniqueidentifier] NOT NULL,
            	[DDD] [int] NOT NULL,
            	[Name] [varchar](100) NOT NULL
                CONSTRAINT [PK_State] PRIMARY KEY ([Id])
            
            );
            """;

        return sql;
    }

    public Task InitializeAsync()
       => _msSqlContainer.StartAsync();

    public Task DisposeAsync()
        => _msSqlContainer.DisposeAsync().AsTask();
}
