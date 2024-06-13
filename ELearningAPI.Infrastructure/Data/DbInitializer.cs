using ELearningAPI.Models.Domain.Entities;
using ELearningAPI.Models.Domain.Enums;

namespace ELearningAPI.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ELearningContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {

           


                return;
            }

            var users = new User[]
            {
                new User { 
                    Id = Guid.NewGuid(), 
                    Email = "homer@fakemail.com", 
                    FirstName="Homer",
                    LastName = "Simpson", 
                    Password = "homerpass", 
                    Type = UserRole.Professor 
                },
               new User {
                    Id = Guid.NewGuid(),
                    Email = "bart@fakemail.com",
                    FirstName="Bart",
                    LastName = "Simpson",
                    Password = "bartpass",
                    Type = UserRole.Student
                }

            };

            context.Users.AddRange(users);

            context.SaveChanges();


        }
    }
}
