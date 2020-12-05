using System;
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
}