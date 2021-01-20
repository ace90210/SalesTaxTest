namespace SalesTaxServices.Models
{
    public class ConfigurationDto
    {
        public decimal SalesTaxRate { get; set; }

        public decimal ImportTaxRate { get; set; }

        public ItemType[] ExcludedSalesTaxItems { get; set; }
    }
}
