using ELearningAPI.Models.Domain.Entities;
using ELearningAPI.Models.Domain.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ELearningAPI.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ELearningContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any() || context.Courses.Any() || context.Lessons.Any() || context.Questions.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User { 
                    Id = 1, 
                    Email = "homer@fakemail.com", 
                    FirstName="Homer",
                    LastName = "Simpson", 
                    Password = "homerpass", 
                    Type = UserRole.Professor 
                },
               new User {
                    Id = 2,
                    Email = "bart@fakemail.com",
                    FirstName="Bart",
                    LastName = "Simpson",
                    Password = "bartpass",
                    Type = UserRole.Student
                }

            };

            context.Users.AddRange(users);


            var courses = new List<Course>()
            {
                new()
                {
                    Id = 1,
                    Title = "Javscript the begining",
                    Description = "Learn Javascript from scrach",
                    CreatedAt = DateTime.Now,
                    CreatedById = 1
                },
                new()
                {
                    Id = 2,
                    Title = "React from 0 to Legend",
                    Description = "Learn react and improve your skillset like a legend!!",
                    CreatedAt = DateTime.Now,
                    CreatedById = 1,
                    CorrelatedCourses = new List<CorrelatedCourse>()
                    {
                        new()
                        {
                            CourseId = 2,
                            CorrelatedCourseId = 1,
                        }
                    }
                }
            };

            var lessons = new List<Lesson>()
            {
                new()
                {
                    Id = 1,
                    Title = "What is a variable?",
                    Description = "lets practice variables",
                    Source = "https://fakestorage.com/resourceasasa",
                    ApprovalThreshold = 40,
                    CourseId= 1,
                    CreatedBy = 1,
                    CreatedAt = DateTime.Now,
                },
               new()
                {
                    Id = 2,
                    Title = "What is the scope?",
                    Description = "lets practice scopes",
                    Source = "https://fakestorage.com/resourceasasa",
                    ApprovalThreshold = 40,
                    CourseId= 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    CorrelatedLessons = new List<CorrelatedLesson>()
                    {
                        new()
                        {
                            LessonId = 2,
                            CorrelatedLessonId = 1
                        }
                    }
                }
            };

            var questions = new List<Question>()
            {

                new()
                {
                    Id = 1,
                    Description = "Define a variable in a programming language",
                    Score = 20,
                    Type = QuestionType.SingleCorrectOption,
                    LessonId = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy= 1,
                    Options = new List<QuestionOption>()
                    {
                        new()
                        {
                            Id= 1,
                            Option = "a banana",
                            IsCorrect = false,
                            QuestionId = 1,

                        },
                       new()
                        {
                            Id= 2,
                            Option = "a tree",
                            IsCorrect = false,
                            QuestionId = 1,
                        },
                       new()
                       {
                           Id=3,
                           Option = "a location in memory with a defined reference where you can store your data",
                           IsCorrect= true,
                           QuestionId= 1
                       }
                    }
                },
               new()
                {
                    Id = 2,
                    Description = "Define a variable in a programming language",
                    Score = 20,
                    Type = QuestionType.SingleCorrectOption,
                    LessonId = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy= 1,
                    Options = new List<QuestionOption>()
                    {
                        new()
                        {
                            Id= 4,
                            Option = "a banana",
                            IsCorrect = false,
                            QuestionId = 1,

                        },
                       new()
                        {
                            Id= 5,
                            Option = "a tree",
                            IsCorrect = false,
                            QuestionId = 1,
                        },
                       new()
                       {
                           Id=6,
                           Option = "a location in memory with a defined reference where you can store your data",
                           IsCorrect= true,
                           QuestionId= 1
                       }
                    }
                }

            };

            var firstOptions = new List<int>() { 3 };

            var secondOptions = new List<int>() { 6 };

            var studentAnswers = new List<StudentAnswer>
            {
                new()
                {
                    Id = 1,
                    UserId = 2,
                    QuestionId = 1,
                    SelectedOptions = JsonSerializer.Serialize(firstOptions)
                },
               new()
                {
                    Id = 2,
                    UserId = 2,
                    QuestionId = 2,
                    SelectedOptions = JsonSerializer.Serialize(secondOptions)
                }
            };



            context.Courses.AddRange(courses);

            context.Lessons.AddRange(lessons);

            context.Questions.AddRange(questions);

            context.StudentAnswers.AddRange(studentAnswers);


            context.SaveChanges();


        }
    }
}
