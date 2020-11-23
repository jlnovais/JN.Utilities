using JN.Utilities.Core.Entities;

namespace JN.Utilities.Core.Services
{
    public interface ISolverService
    {
        ProblemSolution Solve(ProblemConfiguration config);
    }
}