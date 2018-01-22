namespace Dentist.Data.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Dentist.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Dentist.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (!context.Roles.Any())
            {
                var role = context.Roles.Add(new IdentityRole("Admin"));
                var role1 = context.Roles.Add(new IdentityRole("Dentist"));
                context.SaveChanges();
                role = context.Roles.FirstOrDefault();
                var user = context.Users.Single();
                user.Roles.Add(new IdentityUserRole() { RoleId = role.Id,UserId=user.Id });
                context.SaveChanges();

            }
        }
    }
}
