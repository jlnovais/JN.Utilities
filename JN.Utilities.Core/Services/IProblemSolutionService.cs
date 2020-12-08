using System.Threading.Tasks;
using JN.Utilities.Core.Dto;
using JN.Utilities.Core.Entities;

namespace JN.Utilities.Core.Services
{
    public interface IProblemSolutionService
    {
        Task<Result> Save(ProblemSolution solution, string user);
        Task<Result<ProblemSolution>> GetById(string id, string username);
    }
}