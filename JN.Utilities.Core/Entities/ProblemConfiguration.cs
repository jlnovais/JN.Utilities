using System.Collections.Generic;

namespace JN.Utilities.Core.Entities
{
    public class ProblemConfiguration
    {
        public double AvailableAmount { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
