using System;
using System.Collections.Generic;

namespace JN.Utilities.Core.Entities
{
    public class ProblemSolution
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool HasOptimalSolution { get; set; }
        public int TotalVariables { get; set; }
        public int TotalConstraints { get; set; }
        public decimal FinalAmount { get; set; }
        public double RemainingAmount { get; set; }
        public long SolveTimeMs { get; set; }
        public long Iterations { get; set; }
        public long Nodes { get; set; }

        public List<SolutionVariable> ResponseVariables = new List<SolutionVariable>();
    }
}