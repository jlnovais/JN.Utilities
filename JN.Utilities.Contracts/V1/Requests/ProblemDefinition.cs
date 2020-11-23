using System.Collections.Generic;

namespace JN.Utilities.Contracts.V1.Requests
{
    public class ProblemDefinition
    {
        public double AvailableAmount { get; set; }
        public List<ProductDetails> Products { get; set; } = new List<ProductDetails>();
    }
}
