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

                if (!Configuration.ExcludedSalesTaxItems.Contains((ItemType) orderItem.Type)) //TODO Refactor to safer method
                {
                    var salesTax = (Math.Round((int)(100 * rowPrice) * Configuration.SalesTaxRate / 5) * 5) / 100;
                    rowPrice += salesTax;
                    salesTaxTotal += salesTax;
                }
                total += rowPrice;

                Console.WriteLine(string.Format("{0} {1}: £{2:0.00}", orderItem.Quantity, orderItem.Name, rowPrice));
            }


            Console.WriteLine(string.Format("Sales Total: £{0:0.00}", salesTaxTotal));

            Console.WriteLine(string.Format("Total £{0:0.00}", total));
        }
    }
}
