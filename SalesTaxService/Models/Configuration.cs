namespace SalesTaxService.Models
{
    public static class Configuration
    {
        public static decimal SalesTaxRate { get; set; }

        public static decimal ImportRate { get; set; }

        public static ItemType[] ExcludedSalesTaxItems { get; set; }
    }
}
