using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using Dictionary.Common.Infrastructure;
using Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dictionary.Persistence.Context
{
    internal sealed class SeedData
    {
        internal static List<User> GenerateUsers()
        {
            var users = new Faker<User>("tr")
                .RuleFor(e => e.Id, e => Guid.NewGuid())
                .RuleFor(e => e.CreateDate, e => e.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(e=>e.UserName,e=>e.Internet.UserName())
                .RuleFor(e => e.Password, e => PasswordEncryptor.Encrypt(e.Internet.Password()))
                .RuleFor(e=>e.Email,e=>e.Internet.Email())
                .RuleFor(e=>e.EmailConfirmed,e=>e.PickRandom(true,false))
                .Generate(500);

            return users;
        }

        internal static List<Entry> GenerateEntries(IEnumerable<Guid> userIds,List<Guid> guids)
        {
            int counter = 0;
            var entries = new Faker<Entry>("tr")
                .RuleFor(e => e.Id, e => guids[counter++] )
                .RuleFor(e => e.CreateDate, e => e.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(e => e.Subject, e => e.Lorem.Sentence(5,5))
                .RuleFor(e => e.Content, e => e.Lorem.Paragraph(2))
                .RuleFor(e => e.UserId, e => e.PickRandom(userIds))
                .Generate(guids.Count);

            return entries;
        }

        internal static List<EntryComment> GenerateEntryComments(IEnumerable<Guid> userIds,List<Guid> guids)
        {
            int counter = 0;
            var entries = new Faker<EntryComment>("tr")
                .RuleFor(e => e.Id, e => Guid.NewGuid())
                .RuleFor(e => e.CreateDate, e => e.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(e => e.Content, e => e.Lorem.Paragraph(2))
                .RuleFor(e => e.UserId, e => e.PickRandom(userIds))
                .RuleFor(e => e.EntryId, e => e.PickRandom(guids))
                .Generate(guids.Count);

            return entries;
        }

        internal static async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("DictionarySqlServer"));

            using var context = new DictionaryContext(dbContextOptionsBuilder.Options, configuration);

            if (context.Users.Any())
            {
                await Task.CompletedTask;
                return;
            }

            var guids = Enumerable.Range(0, 150).Select(e => Guid.NewGuid()).ToList();

            var users= GenerateUsers();

            var userIds = users.Select(e => e.Id).ToList();


            await context.Users.AddRangeAsync(users);

           var entries= GenerateEntries(userIds,guids);

           await context.Entries.AddRangeAsync(entries);

            var entryComments = GenerateEntryComments(userIds, guids);

            await context.EntryComments.AddRangeAsync(entryComments);

            await context.SaveChangesAsync();


        }
    }
}
