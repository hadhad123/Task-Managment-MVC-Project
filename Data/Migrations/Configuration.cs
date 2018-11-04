namespace Data.Migrations
{
    using IdentityProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            AddRoles(context);
            AddTaskStatuses(context);
            AddAdmin(context);
        }
        private void AddRoles(ApplicationDbContext context)
        {
            List<IdentityRole> Roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Name = "Manager"
                },
                new IdentityRole()
                {
                    Name = "Employee"
                }
            };

            foreach (IdentityRole role in Roles)
            {
                IdentityRole RoleExists = context.Roles.FirstOrDefault(r => r.Name == role.Name);
                if (RoleExists == null)
                    context.Roles.Add(role);
            }
            context.Commit();
        }

        private void AddTaskStatuses(ApplicationDbContext context)
        {
            List<TaskStatus> TaskStatuses = new List<TaskStatus>()
            {
                new TaskStatus()
                {
                    Status = "Assigned"
                },
                new TaskStatus()
                {
                    Status = "InProgress"
                },
                new TaskStatus()
                {
                    Status = "Completed"
                },
                new TaskStatus()
                {
                    Status = "Closed"
                }
            };

            foreach (TaskStatus status in TaskStatuses)
            {
                TaskStatus StatusExists = context.TaskStatuses.FirstOrDefault(s => s.Status == status.Status);
                if (StatusExists == null)
                    context.TaskStatuses.Add(status);
            }
            context.Commit();
        }

        private void AddAdmin(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var PasswordHash = new PasswordHasher();
            if (!context.Users.Any(u => u.UserName == "admin@admin.net"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.net",
                    Email = "admin@admin.net",
                    PasswordHash = PasswordHash.HashPassword("123456")
                };

                UserManager.Create(user);

                IdentityRole ManagerRole = context.Roles.FirstOrDefault(r => r.Name == "Manager");
                UserManager.AddToRole(user.Id, ManagerRole.Id);
            }
        }
    }
}
