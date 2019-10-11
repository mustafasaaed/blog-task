using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Entites;

namespace Task.Data.Initialization
{
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Roles.Add(new IdentityRole { Name = "Admin" });
            var user = new ApplicationUser
            {
                FirstName = "first name",
                LastName = "last name",
                Email = "Admin@admin.com",
                EmailConfirmed = false,
                UserName = "Admin@admin.com",
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.Create(user, "Abcd$$123");
            userManager.AddToRole(user.Id, "Admin");

            var comments = new List<Comment> {
                new Comment { Body = "first comment" },
                new Comment { Body = "second comment"}
                };

            context.Posts.Add(new Post
            {
                Title = "First Post",
                Description = "post body",
                Category = new Category
                {
                    Name = "Programming",
                    IsDeleted = false,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                },
                ImageUrl = "~/Photos/1dd69fa9-184c-4c32-ba78-563e95f0f638.png",
                Comments = comments ,
                IsDeleted = false,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            });

            context.SaveChanges();
        }
    }
}
