namespace JN.Utilities.Contracts.V1.Responses
{
    public class SolutionStatistics
    {
        public long SolveTimeMs { get; set; }
        public long Iterations { get; set; }
        public long Nodes { get; set; }
        public int TotalVariables { get; set; }
        public int TotalConstraints { get; set; }
    }
}