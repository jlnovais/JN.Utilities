namespace JN.Utilities.Core.Entities
{
    public class Product
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string Code { get; set; }
        public int MinUnits { get; set; }
        public int MaxUnits { get; set; }
    }
}
