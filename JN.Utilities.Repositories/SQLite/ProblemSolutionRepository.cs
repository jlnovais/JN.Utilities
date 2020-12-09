using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JN.Utilities.Core.Repositories;
using Microsoft.Data.Sqlite;

namespace JN.Utilities.Repositories.SQLite
{
    public class ProblemSolutionRepository: BaseRepository, IProblemSolutionRepository
    {
        public ProblemSolutionRepository(string connectionString) : base(connectionString)
        {
        }

        public ProblemSolutionRepository(object config) : base(config)
        {
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.EnableExtensions(true);
            //connection.LoadExtension(@"SQLite.Interop.dll", "sqlite3_json_init");
            //connection.LoadExtension("sqlite3_json_init");


            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'ProblemSolutions';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName == "ProblemSolutions")
                return;

            connection.Execute(
                @"CREATE TABLE ProblemSolutions (
	                    Id	TEXT NOT NULL UNIQUE,
                        RequestDate TEXT,
                        Username TEXT,
	                    JsonData TEXT,
	                    PRIMARY KEY(""Id"")
                    );");
        }

        public async Task Save(string key, string item, string username, DateTime requestDate)
        {

            using var connection = new SqliteConnection(ConnectionString);

            var content = new
            {
                id = key,
                requestDate,
                username,
                jsonData = item
            };

            await connection.ExecuteAsync("INSERT INTO ProblemSolutions (Id, RequestDate, Username, JsonData)" +
                                          "VALUES (@id, @requestDate, @username, @jsonData);", content);

        }

        public async Task<string> GetById(string id, string username)
        {
            using var connection = new SqliteConnection(ConnectionString);

            var parameters = new
            {
                id, username
            };

            var res = await connection.QueryAsync<string>("SELECT JsonData FROM ProblemSolutions WHERE Id=@id AND Username = @username;", parameters);

            return res.FirstOrDefault();


        }
    }
}
