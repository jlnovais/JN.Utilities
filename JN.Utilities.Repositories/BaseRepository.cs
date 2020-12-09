using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace JN.Utilities.Repositories
{
    public abstract class BaseRepository
    {
        public string ConnectionString { get; set; }
        public object Config { get; set; }

        protected BaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected BaseRepository(object config)
        {
            Config = config;
        }

        public async Task<int> DeleteByDate(string key, string username, DateTime startDateTime, DateTime endDateTime)
        {
            using var connection = new SqliteConnection(ConnectionString);

            var content = new
            {
                id = key,
                username,
                startDateTime,
                endDateTime
            };

            var res = await connection.ExecuteAsync("DELETE ProblemSolutions " +
                                                    "WHERE Id=@id AND Username = @username AND RequestDate>= @startDateTime AND RequestDate<=@endDateTime", content);

            return res;
        }

        public async Task<int> DeleteByKey(string key, string username)
        {
            using var connection = new SqliteConnection(ConnectionString);

            var content = new
            {
                id = key,
                username
            };

            var res = await connection.ExecuteAsync("DELETE ProblemSolutions " +
                                                    "WHERE Id=@id AND Username = @username;", content);

            return res;
        }
    }
}
