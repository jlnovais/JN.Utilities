using System.Linq;
using Dapper;
using JN.Utilities.Core.Entities;
using Microsoft.Data.Sqlite;

namespace JN.Utilities.Repositories
{
    public interface IProblemSolutionRepository
    {
        void Setup();
        void Save(ProblemSolution item);
        ProblemSolution GetById(string id);
    }

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
	                    JsonData TEXT,
	                    PRIMARY KEY(""Id"")
                    );");
        }

        public void Save(ProblemSolution item)
        {

        }

        public ProblemSolution GetById(string id)
        {
            return null;
        }
    }
}
