using System.Collections.Generic;

namespace JN.Utilities.Core.Entities
{
    public class ProblemSolution
    {
        public bool HasOptimalSolution { get; set; }
        public int NumberVariables { get; set; }
        public int NumConstraints { get; set; }
        public decimal FinalAmount { get; set; }
        public double RemainingAmount { get; set; }
        public long SolveTimeMs { get; set; }
        public long Iterations { get; set; }
        public long Nodes { get; set; }

        public List<SolutionVariable> ResponseVariables = new List<SolutionVariable>();
    }
}