using Newtonsoft.Json;
using SalesTaxServices.Models;
using System;
using System.IO;
using System.Linq;

namespace SalesTaxServices.Services
{
    public class SalesTaxService
    {
        public void InitialiseConfiguration()
        {
            string configurationJson = File.ReadAllText(@"Data/DefaultSettings.json");

            var configuration = JsonConvert.DeserializeObject<ConfigurationDto>(configurationJson);

            Configuration.SalesTaxRate = configuration.SalesTaxRate;
            Configuration.ExcludedSalesTaxItems = configuration.ExcludedSalesTaxItems;
            Configuration.ImportRate = configuration.ImportTaxRate;
        }

        public OrderItem[] LoadOrders(string orderFileName)
        {
            Console.WriteLine($"{orderFileName} output:");
            string ordersJson = File.ReadAllText($"Data/{orderFileName}.json");

            var orders = JsonConvert.DeserializeObject<OrderItem[]>(ordersJson);

            return orders;
        }

        public void ProcessOrders(OrderItem[] orderItems)
        {

            decimal total = 0, salesTaxTotal = 0;
            foreach (var orderItem in orderItems)
            {
                var rowPrice = orderItem.Quantity * orderItem.Price;
                var salesTax = ProcessSalesTax(orderItem, rowPrice);

                rowPrice += salesTax;

                var importTax = ProcessImportTax(orderItem, rowPrice);
                rowPrice += importTax;


                salesTaxTotal += salesTax + importTax; //Description implies no import tax but outputs example shows both

                total += rowPrice;

                Console.WriteLine(string.Format("{0} {1}: £{2:0.00}", orderItem.Quantity, orderItem.Name, rowPrice));
            }

            Console.WriteLine(string.Format("Sales Total: £{0:0.00}", salesTaxTotal));

            Console.WriteLine(string.Format("Total £{0:0.00}\n", total));
        }

        private decimal ProcessImportTax(OrderItem orderItem, decimal rowPrice)
        {
            decimal importTax = 0;
            if (orderItem.Imported) //TODO Refactor to safer method
            {
                importTax = (Math.Round((int)(100 * rowPrice) * Configuration.ImportRate / 5) * 5) / 100;
            }

            return importTax;
        }

        private static decimal ProcessSalesTax(OrderItem orderItem, decimal rowPrice)
        {
            decimal salesTax = 0;
            if (!Configuration.ExcludedSalesTaxItems.Contains((ItemType)orderItem.Type)) //TODO Refactor to safer method
            {
                salesTax = (Math.Round((int)(100 * rowPrice) * Configuration.SalesTaxRate / 5) * 5) / 100;
            }

            return salesTax;
        }


    }
}
