using System.Collections.Generic;
using ClasSy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClasSy.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedRoles(context);
            var passwordHasher = new PasswordHasher();

            var professor = new Professor
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Lejla",
                LastName = "Hodzic",
                BirthDate = new DateTime(1996, 9, 10),
                Email = "lejlasalic96@gmail.com",
                Address = "Igmanskih Bataljona 27",
                BirthPlace = "Sarajevo",
                EmailConfirmed = true,
                PasswordHash = passwordHasher.HashPassword("P@ssw0rd"),
                UserName = "lejlasalic96@gmail.com",
                PhoneNumber = "061184001",
                SecurityStamp = "dcvfgdve",
                PhoneNumberConfirmed = true,
                ClassTeacher = 1,
            };
            var schoolClass = new SchoolClass
            {
                Professor = professor,
                Name = "Literature",
                Department = "English Language"
            };
            var parent = new Parent
            {
                FirstName = "Babo",
                LastName = "Babic",
                Address = "Kuca Moja 2",
                BirthDate = new DateTime(1970,4,26),
                Email = "babo@babo.babo",
                BirthPlace = "Babovina",
                PasswordHash = passwordHasher.HashPassword("P@ssw0rd"),
                UserName = "babo@babo.babo",
                PhoneNumber = "061265495",
                SecurityStamp = "dcvfgdve",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                
            };
            var student = new Student
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Emir",
                LastName = "Kurtanovic",
                BirthDate = new DateTime(1996, 7, 6),
                Email = "emir.kurtanovic@gmail.com",
                EmailConfirmed = true,
                Address = "Hadzickih Bataljona 27",
                SchoolClass = schoolClass,
                PhoneNumber = "061123456",
                BirthPlace = "Sarajevo",
                PhoneNumberConfirmed = true,
                ClassPresident = 1,
                UserName = "emir.kurtanovic@gmail.com",
                PasswordHash = passwordHasher.HashPassword("P@ssw0rd"),
                SecurityStamp = "dcvfgdve",
                Parents = new List<Parent>() { parent}
                
            };
            context.Users.AddOrUpdate(professor);
            context.SchoolClasses.AddOrUpdate(schoolClass);
            context.Users.AddOrUpdate(parent);
            context.Users.AddOrUpdate(student);
            context.SaveChanges();

            var userStore = new UserStore<Professor>(context);
            var userManager = new UserManager<Professor>(userStore);

            userManager.AddToRole(professor.Id, RoleName.Professor);

            var userStore2 = new UserStore<Parent>(context);
            var userManager2 = new UserManager<Parent>(userStore2);

            userManager2.AddToRole(parent.Id, RoleName.Parent);

            var userStore3 = new UserStore<Student>(context);
            var userManager3 = new UserManager<Student>(userStore3);

            userManager3.AddToRole(student.Id, RoleName.Student);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method C:\Git\ClasSy\ClasSy\fonts\
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
        private void SeedRoles(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var professorRole = new IdentityRole { Name = RoleName.Professor };
                roleManager.Create(professorRole);

                var adminRole = new IdentityRole { Name = RoleName.Admin };
                roleManager.Create(adminRole);

                var studentRole = new IdentityRole { Name = RoleName.Student };
                roleManager.Create(studentRole);

                var parentRole = new IdentityRole { Name = RoleName.Parent };
                roleManager.Create(parentRole);

                context.SaveChanges();
            }
        }
    }
}
