using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Persistence
{
    public static class Seeder
    {
        public static Task Seed(this TaskTrackerDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Tasks.Any())
            {
                var faker = new Faker<TaskItem>()
                    .RuleFor(t => t.Id, f => Guid.NewGuid())
                    .RuleFor(t => t.Title, f => f.Lorem.Sentence(5))
                    .RuleFor(t => t.Description, f => f.Lorem.Paragraph())
                    .RuleFor(t => t.DueDate, f => f.Date.Future().ToUniversalTime())
                    .RuleFor(t => t.IsCompleted, f => f.Random.Bool());

                var fakeData = faker.Generate(50);
                context.Tasks.AddRange(fakeData);
                context.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}