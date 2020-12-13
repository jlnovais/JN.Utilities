using System;
using System.Threading.Tasks;

namespace JN.Utilities.Core.Repositories
{
    public interface IProblemSolutionRepository
    {
        void Setup();
        Task Save(string key, string item, string username, DateTime requestDate);
        Task<string> GetById(string id, string username);
        Task<int> DeleteByDate(string key, string username, DateTime startDateTime, DateTime endDateTime);
        Task<int> DeleteByKey(string key, string username);
    }
}