using System.Collections.Generic;

namespace JN.Utilities.Contracts.V1.Responses
{
    public class Solution
    {
        public string Id { get; set; }
        public bool HasOptimalSolution { get; set; }
        public decimal FinalAmount { get; set; }
        public double RemainingAmount { get; set; }
        public SolutionStatistics Statistics { get; set; }
        public List<SolutionItem> SolutionItems = new List<SolutionItem>();
    }

    public class SolutionStatistics
    {
        public long SolveTimeMs { get; set; }
        public long Iterations { get; set; }
        public long Nodes { get; set; }
        public int NumberVariables { get; set; }
        public int NumConstraints { get; set; }
    }
}