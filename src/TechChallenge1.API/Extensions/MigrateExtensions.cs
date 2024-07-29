using Microsoft.EntityFrameworkCore;
using TechChallenge1.Data.Context;

namespace TechChallenge1.API.Extensions
{
    public static class MigrateExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using techchallengeDbContext context =
                scope.ServiceProvider.GetRequiredService<techchallengeDbContext>();

            context.Database.Migrate();

            //if (context.States.Any())
            //{
            //    return;
            //}

            //context.States.AddRange(StateList.List);

            //context.SaveChanges();
        }
    }
}
