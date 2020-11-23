namespace JN.Utilities.Contracts.V1.Requests
{
    public class ProductDetails
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string Code { get; set; }
        public int MinUnits { get; set; }
        public int MaxUnits { get; set; }
    }
}
