namespace JN.Utilities.Contracts.V1.Responses
{
    public class SolutionItem
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public double SolutionValue { get; set; }
        public decimal UnitPrice { get; set; }
        public double FinalAmount { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }

    }
}