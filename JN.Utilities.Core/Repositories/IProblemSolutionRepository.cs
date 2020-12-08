using System;
using System.Threading.Tasks;

namespace JN.Utilities.Core.Repositories
{
    public interface IProblemSolutionRepository
    {
        void Setup();
        Task Save(string key, string item, string username, DateTime requestDate);
        Task<string> GetById(string id, string username);
    }
}