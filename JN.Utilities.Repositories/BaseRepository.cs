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

 
    }
}
