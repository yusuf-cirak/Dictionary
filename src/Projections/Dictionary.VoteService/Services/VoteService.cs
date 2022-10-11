using Dapper;
using Dictionary.Common.Enums;
using Dictionary.Common.Events;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Dictionary.VoteService.Services
{
    public sealed class VoteService
    {
        private string connectionString;
        private readonly IConfiguration _configuration;

        public VoteService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DictionarySqlServer");
        }

        public async Task DeleteFromEntryVoteAsync(DeleteEntryVoteEvent @event)
        {
            using var sqlConnection = new SqlConnection(connectionString);
            //connection.CreateCommand().ExecuteNonQueryAsync("");

            await sqlConnection
                .ExecuteAsync("DELETE FROM EntryVotes WHERE EntryId=@EntryId AND UserId=@UserId", new
                {
                    EntryId = @event.EntryId,
                    UserId = @event.UserId
                });
        }

        public async Task CreateEntryVoteAsync(CreateEntryVoteEvent @event)
        {
            await DeleteFromEntryVoteAsync(
                new DeleteEntryVoteEvent { EntryId = @event.EntryId, UserId = @event.UserId });

            using var sqlConnection = new SqlConnection(connectionString);

            await sqlConnection
                .ExecuteAsync(
                "INSERT INTO EntryVotes (Id,EntryId,UserId,VoteType,CreateDate) VALUES (@Id,@EntryId,@UserId,@VoteType,GETDATE())",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryId = @event.EntryId,
                    UserId = @event.UserId,
                    VoteType = (int)@event.VoteType
                });
        }
    }
}