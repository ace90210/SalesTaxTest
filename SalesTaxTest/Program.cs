using SalesTaxServices.Services;
using System;

namespace SalesTaxTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var salesService = new SalesTaxService();
            salesService.InitialiseConfiguration();
            var ordersToProcess = salesService.LoadOrders("Sample3");

            salesService.ProcessOrders(ordersToProcess);

            Console.WriteLine("Hello World!");
        }
    }
}
