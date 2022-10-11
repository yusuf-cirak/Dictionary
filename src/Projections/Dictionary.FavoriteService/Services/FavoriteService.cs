using Dapper;
using Dictionary.Common.Events;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictionary.FavoriteService.Services
{
    public sealed class FavoriteService
    {
        private readonly string connectionString;

        public FavoriteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryFav(CreateEntryFavoriteEvent @event){

            using var connection = new SqlConnection(connectionString);

            //connection.CreateCommand().ExecuteNonQueryAsync("");

           await connection.ExecuteAsync("INSERT INTO EntryFavorites (Id,EntryId,UserId,CreateDate) VALUES (@Id,@EntryId,@UserId,GETDATE())", new
           {
               Id=Guid.NewGuid(),
               EntryId=@event.EntryId,
               UserId=@event.UserId
           });

        }
        
        public async Task DeleteEntryFav(DeleteEntryFavoriteEvent @event){

            using var connection = new SqlConnection(connectionString);

            //connection.CreateCommand().ExecuteNonQueryAsync("");

            await connection.ExecuteAsync("DELETE FROM EntryFavorites WHERE EntryId=@EntryId AND UserId=@UserId  ", new
            {
                EntryId=@event.EntryId,
                UserId=@event.UserId
            });

        }
    }
}